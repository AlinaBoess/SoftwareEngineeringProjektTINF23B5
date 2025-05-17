"use client";
import React from "react";
import { useState, useEffect } from "react";
import { useRouter } from 'next/navigation';

function Homepage() {
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";
    const [authModalOpen, setAuthModalOpen] = useState(false);
    const [authMode, setAuthMode] = useState("login");
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [isAuthChecking, setIsAuthChecking] = useState(true);
    const router = useRouter();

 

    useEffect(() => {
        let isMounted = true;

        const checkAuth = async () => {
            try {
                const res = await fetch(`${API_URL}/api/User`, {
                    credentials: "include",
                    headers: {
                        'Accept': 'application/json',
                    }
                });
                console.log('Auth check response status:', res.status);
                console.log('Headers:', res.headers);

                if (!isMounted) return;

                if (res.ok) {
                    const data = await res.json();
                    setUser(data);
                    
                } else {
                    setUser(null);
                }
            } catch (err) {
                console.error("Auth check failed:", err);
                setUser(null);
            } finally {
                if (isMounted) {
                    setIsAuthChecking(false);
                }
            }
            
        };
        console.log("Checking auth...");
        checkAuth();
        return () => {
            isMounted = false;
            

        };

    }, []);

    const handleLogout = async () => {
        try {
            await fetch(`${API_URL}/api/Auth/logout`, {
                method: "POST",
                credentials: "include",
            });
            setUser(null);
            window.location.reload();


        } catch (err) {
            console.error("Logout failed:", err);
        }
    };

    const handleAuthSubmit = async (e) => {
        e.preventDefault();
        console.log('form submitted');
        const form = e.target;
        const firstName = form.firstName?.value;
        const lastName = form.lastName?.value;
        const email = form.email.value;
        const password = form.password.value;

        setIsLoading(true);
        try {
            const endpoint = authMode === "login"
                ? `${API_URL}/api/Auth/login`
                : `${API_URL}/api/Auth/register`;

            let requestBody;

            if (authMode === "login") {
                requestBody = { email, password };
            } else {
                requestBody = {
                    firstName,
                    lastName,
                    email,
                    password
                };
            }

            const response = await fetch(endpoint, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include",
                body: JSON.stringify(requestBody),
            });
            console.log('Login response status:', response.status);
            console.log('Set-Cookie header:', response.headers.get('set-cookie'));


            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Authentication failed');
            }

            const data = await response.json();

            // Store data
            setUser(data); // Since the API returns { email, password }

            setAuthModalOpen(false);

            // Force a refresh of the auth state
            //checkAuth();
            router.refresh();
        } catch (error) {
            alert(error.message);
            console.error('Authentifizierungsfehler:', error);
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
                    {isAuthChecking ? (
                        <div>Loading...</div>
                    ) : user ? (
                        <>
                                <span className="text-[#e6b17e]">Willkommen, {user.email}</span>
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
