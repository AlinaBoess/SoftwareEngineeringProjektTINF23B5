"use client";
// AuthContext.js
import React, { createContext, useState, useEffect, useContext } from 'react';
const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(true); // Start true on initial load

    useEffect(() => {
        // This is your initial auth check logic, moved here
        let isMounted = true;
        setIsLoading(true);
        const storedUser = localStorage.getItem('authUser');
        if (storedUser) {
            try {
                setUser(JSON.parse(storedUser));
            } catch (e) {
                localStorage.removeItem('authUser');
            }
        }

        fetch(`${API_URL}/api/User`, { credentials: 'include', headers: { Accept: 'application/json' } })
            .then(res => {
                if (!isMounted) return null;
                if (res.ok) return res.json();
                // More careful removal: only on explicit unauth, or if no prior storedUser
                if (res.status === 401 || res.status === 403 || !storedUser) {
                    localStorage.removeItem('authUser');
                    setUser(null);
                }
                return null; // Or throw an error to be caught
            })
            .then(data => {
                if (data && isMounted) {
                    setUser(data);
                    localStorage.setItem('authUser', JSON.stringify(data));
                }
            })
            .catch(err => {
                if (isMounted) {
                    console.error('Auth check failed:', err);
                    if (!storedUser) { // Or always if fetch fails
                        localStorage.removeItem('authUser');
                        setUser(null);
                    }
                }
            })
            .finally(() => {
                if (isMounted) setIsLoading(false);
            });

        return () => { isMounted = false; };
    }, []);

    const login = async (credentials) => {
        // Your login logic: fetch, setUser, localStorage.setItem
        // Example:
        const res = await fetch(`${API_URL}/api/Auth/login`, { /* ... */ body: JSON.stringify(credentials) });
        const data = await res.json();
        if (res.ok) {
            setUser(data);
            localStorage.setItem('authUser', JSON.stringify(data));
            return data;
        } else {
            throw new Error(data.message || 'Login failed');
        }
    };

    const logout = async () => {
        // Your logout logic: fetch, setUser(null), localStorage.removeItem
        await fetch(`${API_URL}/api/Auth/logout`, { method: 'POST', credentials: 'include' });
        setUser(null);
        localStorage.removeItem('authUser');
    };

    // Listen for storage events for cross-tab sync
    useEffect(() => {
        const syncAuth = (event) => {
            if (event.key === 'authUser') {
                const newStoredUser = localStorage.getItem('authUser');
                setUser(newStoredUser ? JSON.parse(newStoredUser) : null);
            }
        };
        window.addEventListener('storage', syncAuth);
        return () => window.removeEventListener('storage', syncAuth);
    }, []);


    return (
        <AuthContext.Provider value={{ user, setUser, isLoading, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);