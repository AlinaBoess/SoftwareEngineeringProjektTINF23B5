"use client";
import { useRouter } from "next/navigation";
import { useState, useEffect } from "react";

function AddRestaurantForm() {
    // State-Variablen für Formularfelder und Benutzerstatus
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
    //const [isMenuOpen, setIsMenuOpen] = useState(false);

    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";

    // Authentifizierten Benutzer aus dem lokalen speicher laden
    useEffect(() => {
        let isMounted = true;

        const storedUser = localStorage.getItem("authUser");
        if (storedUser && isMounted) {
            setUser(JSON.parse(storedUser));
        }
    }, []);

    // Authentifizierungstatus mit den Server Synchronisieren
    useEffect(() => {
        const checkAuth = async () => {
            try {
                const res = await fetch(`${API_URL}/api/User`, {
                    credentials: "include",
                    headers: { Accept: "application/json" },
                });

                //if (!isMounted) return;

                if (res.ok) {
                    const data = await res.json();
                    setUser(data);
                    localStorage.setItem("authUser", JSON.stringify(data));
                } else {
                    setUser(null);
                    localStorage.removeItem("authUser");
                }
            } catch (err) {
                //if (isMounted) {
                console.error("Auth check failed:", err);
                setUser(null);
                localStorage.removeItem("authUser");
                //}
            }
        };

        checkAuth();
    }, []);

        //return () => {
            //isMounted = false;
        //};
    //}, []);

    // Authentifizierungsstatus bei änderungen synchronisieren
    useEffect(() => {
        const syncAuthState = () => {
            const storedUser = localStorage.getItem("authUser");
            setUser(storedUser ? JSON.parse(storedUser) : null);
        };

        window.addEventListener("storage", syncAuthState);
        return () => window.removeEventListener("storage", syncAuthState);
    }, []);

   
    //useEffect(() => {
        //if (typeof window !== "undefined") {
            //window.localStorage.setItem("authState", JSON.stringify(user));
        //}
    //}, [user]);

    // Logout - Funktion
    const handleLogout = async () => {
        try {
            await fetch(`${API_URL}/api/Auth/logout`, {
                method: "POST",
                credentials: "include",
            });
            setUser(null);
        } catch (err) {
            console.error("Fehler beim Logout:", err);
        }
    };

    // Authenthifizierungsformular absenden
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
                authMode === "login"
                    ? `${API_URL}/api/Auth/login`
                    : `${API_URL}/api/Auth/register`;

            const requestBody =
                authMode === "login"
                    ? { email, password }
                    : { firstName, lastName, email, password };

            const response = await fetch(endpoint, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include",
                body: JSON.stringify(requestBody),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Authentifizierung fehlgeschlagen");
            }

            const data = await response.json();
            setUser(data);
            localStorage.setItem("authUser", JSON.stringify(data));
            setAuthModalOpen(false);
        } catch (error) {
            alert(error.message);
            console.error("Authentifizierungsfehler:", error);
        } finally {
            setIsLoading(false);
        }
    };

    // Formular zum hinzufügen eines Restaurants absenden
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!name || !address || !openingHours || !website) {
            setMessage("Bitte füllen Sie alle Felder aus.");
            return;
        }

        const formData = new FormData();
        formData.append("name", name);
        formData.append("address", address);
        formData.append("openingHours", openingHours);
        formData.append("website", website);
        if (image) formData.append("image", image);

        try {
            const response = await fetch(`${API_URL}/api/restaurants`, {
                method: "POST",
                body: formData,
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Fehler beim Hinzufügen des Restaurants");
            }

            setMessage("Restaurant erfolgreich hinzugefügt!");
            setName("");
            setAddress("");
            setOpeningHours("");
            setWebsite("");
            setImage(null);
        } catch (error) {
            console.error("Fehler beim Hinzufügen des Restaurants:", error);
            setMessage("Fehler beim Hinzufügen des Restaurants.");
        }
    };

    return (
        <div className="min-h-screen bg-[#f5f1e9]">
            {/* Navigation */}
            <nav className="bg-[#2c1810] p-4">
                <div className="container mx-auto flex justify-between items-center">
                    <h1
                        onClick={() => router.push("/")}
                        className="text-3xl font-bold text-[#e6b17e] cursor-pointer hover:text-[#f5f1e9]"
                    >
                        Restaurant Finder
                    </h1>
                    <div className="flex items-center gap-4">
                        {user ? (
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

            {/* Authentifizierungsmodal */}
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
                                    onClick={() =>
                                        setAuthMode(authMode === "login" ? "register" : "login")
                                    }
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
                                                <svg
                                                    className="animate-spin -ml-1 mr-2 h-4 w-4 text-white"
                                                    xmlns="http://www.w3.org/2000/svg"
                                                    fill="none"
                                                    viewBox="0 0 24 24"
                                                >
                                                    <circle
                                                        className="opacity-25"
                                                        cx="12"
                                                        cy="12"
                                                        r="10"
                                                        stroke="currentColor"
                                                        strokeWidth="4"
                                                    ></circle>
                                                    <path
                                                        className="opacity-75"
                                                        fill="currentColor"
                                                        d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
                                                    ></path>
                                                </svg>
                                                {authMode === "login" ? "Einloggen..." : "Registrieren..."}
                                            </span>
                                        ) : authMode === "login" ? "Einloggen" : "Registrieren"}
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            )}


            {/* Formular zum Hinzufügen eines Restaurants */}
            <div className="container mx-auto px-4 py-16 max-w-xl">
                <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                    Neues Restaurant hinzufügen
                </h2>
                {message && (
                    <div className="mb-4 text-center text-[#2c1810] font-medium">
                        {message}
                    </div>
                )}

                <form onSubmit={handleSubmit} className="bg-white p-6 rounded shadow">
                    <div className="mb-4">
                        <label className="block mb-2 text-[#2c1810] font-semibold">
                            Name des Restaurants
                        </label>
                        <input
                            type="text"
                            className="w-full p-2 border rounded"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block mb-2 text-[#2c1810] font-semibold">
                            Adresse
                        </label>
                        <input
                            type="text"
                            className="w-full p-2 border rounded"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                            required
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block mb-2 text-[#2c1810] font-semibold">
                            Öffnungszeiten
                        </label>
                        <input
                            type="text"
                            className="w-full p-2 border rounded"
                            value={openingHours}
                            onChange={(e) => setOpeningHours(e.target.value)}
                            required
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block mb-2 text-[#2c1810] font-semibold">
                            Webseiten-URL
                        </label>
                        <input
                            type="text"
                            className="w-full p-2 border rounded"
                            value={website}
                            onChange={(e) => setWebsite(e.target.value)}
                            required
                        />
                    </div>

                    <div className="mb-4">
                        <label className="block mb-2 text-[#2c1810] font-semibold">
                            Bild
                        </label>
                        <input
                            type="file"
                            accept="image/*"
                            className="w-full p-2 border rounded"
                            onChange={(e) => setImage(e.target.files[0])}
                        />
                    </div>

                    <div className="flex justify-end">
                        <button
                            type="submit"
                            className="bg-[#2c1810] text-white px-4 py-2 rounded hover:bg-[#3d251c]"
                        >
                            Restaurant hinzufügen
                        </button>
                    </div>
                </form>
            </div>

            {/* Fußzeile */}
            <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
                <div className="container mx-auto text-center">
                    <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
                </div>
            </footer>
        </div>
    );
}

export default AddRestaurantForm;
