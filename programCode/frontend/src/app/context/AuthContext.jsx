import { createContext, useContext, useEffect, useState } from "react";
import axios from "axios";

// Kontext erstellen
const AuthContext = createContext();
export const useAuth = () => useContext(AuthContext);

// Provider-Komponente
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    // Benutzer bei Initialisierung laden
    const fetchUser = async () => {
        try {
            const res = await axios.get("/api/me", { withCredentials: true });
            setUser(res.data.user);
        } catch {
            setUser(null);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUser();
    }, []);

    const logout = async () => {
        try {
            await axios.post("/api/logout", {}, { withCredentials: true });
            setUser(null);
        } catch (err) {
            console.error("Fehler beim Logout:", err);
        }
    };

    return (
        <AuthContext.Provider value={{ user, setUser, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};
