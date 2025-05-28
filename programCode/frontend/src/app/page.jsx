/**
 * @fileoverview Homepage der Webanwendung
 *
 * @author Alina Böß <alinaboess05@gmail.com>
 * @author Yahya Ezz Edin <yaezzedin@gmail.com>
 * @author Moumen Kheto <kheto.moumen@gmail.com>
 * @created 2025-04-17
 */

"use client";
import React from "react";
import { useState, useEffect } from "react";
import { useRouter } from 'next/navigation';
import { useAuth } from '@/app/context/AuthContext';

function Homepage() {
    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [openingHours, setOpeningHours] = useState("");
    const [website, setWebsite] = useState("");
    const [image, setImage] = useState(null);
    const [message, setMessage] = useState("");
    const [authModalOpen, setAuthModalOpen] = useState(false);
    const [authMode, setAuthMode] = useState("login");
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(false);


    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";
  

 

    useEffect(() => {
        let isMounted = true;

        if (!localStorage.getItem('authUser')  || localStorage.getItem('authUser') == "null") return
        const storedUser = localStorage.getItem('authUser');
        if (storedUser && isMounted) setUser(JSON.parse(storedUser));

        (async () => {
            try {
                const res = await fetch(`${API_URL}/api/User/Me`, {
                    method: 'GET',
                    headers: {
                        Accept: 'application/json',
                        Authorization: `Bearer ${JSON.parse(localStorage.getItem('authUser')).token}`
                    },
                });

                if (!isMounted) return;

                if (res.ok) {
                    const data = await res.json();
                    setUser(data);
                    localStorage.setItem('authUser', JSON.stringify(data));
                } else {
                    setUser(null);
                    localStorage.removeItem('authUser');
                }
            } catch (err) {
                console.error('Auth check failed:', err);
                setUser(null);
                localStorage.removeItem('authUser');
            }
        })();

        return () => { isMounted = false; };
    }, []);
    // Authentifizierungstatus mit den Server Synchronisieren
    //useEffect(() => {
    //    const checkAuth = async () => {
    //        if(!localStorage.getItem('authUser')) return
    //        try {
    //            const res = await fetch(`${API_URL}/api/User/Me`, {
    //                credentials: "include",
    //                headers: {
    //                    Accept: "application/json",
    //                    Authorization: `Bearer ${JSON.parse(localStorage.getItem('authUser')).token}`
    //                },
    //            });

    //            //if (!isMounted) return;

    //            if (res.ok) {
    //                const data = await res.json();
    //                setUser(data);
    //                localStorage.setItem("authUser", JSON.stringify(data));
    //            } else {
    //                setUser(null);
    //                localStorage.removeItem("authUser");
    //            }
    //        } catch (err) {
    //            //if (isMounted) {
    //            console.error("Auth check failed:", err);
    //            setUser(null);
    //            localStorage.removeItem("authUser");
    //            //}
    //        }
    //    };

    //    checkAuth();
    //}, []);

    // Listen for cross-tab login/logout
    useEffect(() => {
        const syncAuth = () => {
            const stored = localStorage.getItem('authUser');
            setUser(stored ? JSON.parse(stored) : null);
        };
        window.addEventListener('storage', syncAuth);
        return () => window.removeEventListener('storage', syncAuth);
    }, []);

    // Persist latest user object in localStorage for quick boot-up
    useEffect(() => {
        if (typeof window !== 'undefined') {
            localStorage.setItem('authUser', JSON.stringify(user));
        }
    }, [user]);

    // ────────────────────────────────────────────────────────────
    // Handlers
    // ────────────────────────────────────────────────────────────
    const handleLogout = async () => {
        try {
            await fetch(`${API_URL}/api/Auth/logout`, { method: 'POST', credentials: 'include' });
            setUser(null);
            localStorage.removeItem('authUser');
        } catch (err) {
            console.error('Fehler beim Logout:', err);
        }
    };

    const handleAuthSubmit = async (e) => {
        e.preventDefault();
        const form = e.target;
        const firstName = form.firstName?.value;
        const lastName = form.lastName?.value;
        const email = form.email.value;
        const password = form.password.value;

        setIsLoading(true);
        try {
            const endpoint =
                authMode === 'login'
                    ? `${API_URL}/api/Auth/login`
                    : `${API_URL}/api/Auth/register`;

            const body =
                authMode === 'login'
                    ? { email, password }
                    : { firstName, lastName, email, password };

            const res = await fetch(endpoint, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify(body),
            });

            if (!res.ok) {
                /** Expect backend to send { message } */
                const { message } = await res.json();
                throw new Error(message ?? 'Authentifizierung fehlgeschlagen');
            }

            const data = await res.json();   // user DTO returned by the backend
            setUser(data);
            localStorage.setItem('authUser', JSON.stringify(data));
            setAuthModalOpen(false);
        } catch (err) {
            alert(err.message);
            console.error('Authentifizierungsfehler:', err);
        } finally {
            setIsLoading(false);
        }
    };
        

    


    return (
  
    <div className="min-h-screen bg-[#f5f1e9]">
      <nav className="bg-[#2c1810] p-4">  <div>
        <div className="container mx-auto flex justify-between items-center">
          <h1 className="text-[#e6b17e] text-2xl font-playfair">Home</h1>
          <a href="/resowner" className="text-[#e6b17e] hover:text-[#f5f1e9]">
          Restaurant owners form
          </a>
        </div>
                <div className="flex items-center gap-4">
                    {user ? (
                        <>
                            <span className="text-[#e6b17e]">Willkommen, {user.user.firstName}</span>
                            <button
                                onClick={handleLogout}
                                className="text-[#e6b17e] hover:text-[#f5f1e9]"
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <button
                            onClick={() => {
                                setAuthMode("login");
                                setAuthModalOpen(true);
                            }}
                            className="text-[#e6b17e] hover:text-[#f5f1e9]"
                        >
                            Login / Registrieren
                        </button>
                    )}
                    <button
                        onClick={() => setIsMenuOpen(!isMenuOpen)}
                        className="md:hidden text-[#e6b17e]"
                    >
                        <i className="fas fa-bars text-2xl"></i>
                    </button>
                </div>
        </div>
      </nav>

      <header className="text-center py-20">
        <h2 className="text-4xl font-playfair text-[#2c1810] mb-4">
          Willkommen auf unserer Homepage
        </h2>
        <p className="text-[#5c3d2e] mb-8">
          Entdecken Sie die besten kulinarischen Erlebnisse in Ihrer Nähe.
        </p>
        <a
          href="/reservation"
          className="bg-[#2c1810] text-white py-3 px-6 rounded hover:bg-[#3d251c]"
        >
          Jetzt reservieren
        </a>
      </header>

      <section className="bg-[#2c1810] text-[#f5f1e9] py-16 px-4">
        <div className="container mx-auto">
          <h2 className="text-3xl font-playfair text-center mb-8">Über uns</h2>
          <p className="max-w-2xl mx-auto text-center">
            Wir sind leidenschaftliche Gastronomen, die das Ziel haben, das
            Reservierungserlebnis so einfach und angenehm wie möglich zu machen.
          </p>
        </div>
            </section>
            {authModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
                    <div className="bg-white p-8 rounded-lg max-w-sm w-full">
                        <h3 className="text-2xl font-playfair mb-4 text-center">
                            {authMode === "login" ? "Login" : "Registrieren"}
                        </h3>
                        <form onSubmit={handleAuthSubmit} className="grid gap-4">
                            {authMode === "register" && (
                                <>
                                    <input
                                        name="firstName"
                                        type="text"
                                        placeholder="Vorname"
                                        className="w-full p-2 border rounded"
                                        required
                                    />
                                    <input
                                        name="lastName"
                                        type="text"
                                        placeholder="Nachname"
                                        className="w-full p-2 border rounded"
                                        required
                                    />
                                </>
                            )}
                            <input
                                name="email"
                                type="email"
                                placeholder="E-Mail"
                                className="w-full p-2 border rounded"
                                required
                            />
                            <input
                                name="password"
                                type="password"
                                placeholder="Passwort"
                                className="w-full p-2 border rounded"
                                required
                                minLength="6"
                                id="pwd"
                            />
                            <div className="flex justify-between items-center">
                                <button
                                    type="button"
                                    onClick={() => setAuthMode(authMode === "login" ? "register" : "login")}
                                    className="text-sm text-[#2c1810] underline"
                                >
                                    {authMode === "login"
                                        ? "Noch kein Konto? Registrieren"
                                        : "Bereits registriert? Login"}
                                </button>
                                <div className="flex gap-2">
                                    <button
                                        type="button"
                                        onClick={() => setAuthModalOpen(false)}
                                        className="px-4 py-2 text-gray-600 hover:bg-gray-100 rounded"
                                        disabled={isLoading}
                                    >
                                        Abbrechen
                                    </button>
                                    <button
                                        type="submit"
                                        className="px-4 py-2 bg-[#2c1810] text-white rounded hover:bg-[#3d251c] disabled:opacity-50"
                                        disabled={isLoading}
                                    >
                                        {isLoading ? (
                                            <span className="flex items-center justify-center">
                                                <svg className="animate-spin -ml-1 mr-2 h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                                    <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                                                    <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                                                </svg>
                                                {authMode === "login" ? "Einloggen..." : "Registrieren..."}
                                            </span>
                                        ) : (
                                            authMode === "login" ? "Einloggen" : "Registrieren"
                                        )}
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            )}
      <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
        <div className="container mx-auto text-center">
          <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
        </div>
      </footer>
    </div>
  );
}

export default Homepage;
