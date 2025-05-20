"use client";
import { createContext, useContext, useEffect, useState } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";

const AuthContext = createContext();
export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";

    // Initialize axios defaults
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        }
    }, []);

    const setAuthHeader = (token) => {
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            localStorage.setItem('token', token);
        } else {
            delete axios.defaults.headers.common['Authorization'];
            localStorage.removeItem('token');
        }
    };

    const fetchUser = async () => {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                setLoading(false);
                return;
            }

            const res = await axios.get(`${API_URL}/api/User`);
            setUser({
                id: res.data.id,
                name: res.data.name,
                email: res.data.email
            });
        } catch (err) {
            console.error("User fetch error:", err);
            setAuthHeader(null);
            setUser(null);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUser();
    }, []);

    const login = async (email, password) => {
        try {
            const res = await axios.post(`${API_URL}/api/Auth/login`, {
                email,
                password
            });

            setAuthHeader(res.data.token);
            await fetchUser();
            return { success: true };
        } catch (err) {
            console.error("Login error:", err);
            return {
                success: false,
                message: err.response?.data?.message || 'Login failed'
            };
        }
    };

    const register = async (firstName, lastName, email, password) => {
        try {
            const res = await axios.post(`${API_URL}/api/Auth/register`, {
                firstName,
                lastName,
                email,
                password
            });
            return {
                success: true,
                message: res.data.message || 'Registration successful'
            };
        } catch (err) {
            console.error("Registration error:", err);
            return {
                success: false,
                message: err.response?.data?.message || 'Registration failed'
            };
        }
    };

    const logout = async () => {
        try {
            await axios.post(`${API_URL}/api/Auth/logout`);
            setAuthHeader(null);
            setUser(null);
            router.push('/');
        } catch (err) {
            console.error("Logout error:", err);
        }
    };

    const value = {
        user,
        loading,
        login,
        register,
        logout,
        fetchUser
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};