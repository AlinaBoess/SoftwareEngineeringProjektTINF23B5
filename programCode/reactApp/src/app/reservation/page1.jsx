﻿"use client";
import React from "react";
import { useState, useEffect } from "react";
import { useRouter } from 'next/navigation';

function MainComponent() {
    const [authModalOpen, setAuthModalOpen] = useState(false);
    const [authMode, setAuthMode] = useState("login");
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [selectedDate, setSelectedDate] = useState("");
    const [selectedTime, setSelectedTime] = useState("");
    const [selectedGuests, setSelectedGuests] = useState(2);
    const [selectedRestaurant, setSelectedRestaurant] = useState(null);
    const [reservationModal, setReservationModal] = useState(false);
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [phone, setPhone] = useState("");
    const [selectedTable, setSelectedTable] = useState(null);
    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";
    //...
    useEffect(() => {
        async function checkAuth() {
            try {
                const res = await fetch(`${API_URL}/api/User`, {
                    credentials: "include",
                    headers: {
                        'Accept': 'application/json',
                    }
                });

                if (res.status === 401) {
                    // Not authenticated
                    setUser(null);
                    return;
                }

                if (!res.ok) {
                    throw new Error(`HTTP error! status: ${res.status}`);
                }

                const data = await res.json();
                setUser(data);
            } catch (err) {
                console.error("Authentication check failed:", err);
                setUser(null);
            }
        }
        //const { user, loading, login, register, logout } = useAuth();
        //async function checkAuth() {
          //  try {
            //    const res = await fetch(`${API_URL}/api/User`, {
              //      credentials: "include",
                //});
                //if (res.ok) {
                  //  const data = await res.json();
                    //setUser(data);
                //}
            //} catch (err) {
             //   console.error("Fehler beim Abrufen des Benutzers:", err);
        //}
//    }
        checkAuth();
    }, []);

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

    const handleAuthSubmit = async (e) => {
        e.preventDefault();
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
            // Debug cookies
            console.log("Response headers:", [...response.headers.entries()]);

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Authentifizierung fehlgeschlagen');
            }

            const data = await response.json();
            setUser(data);
            setAuthModalOpen(false);
        } catch (error) {
            alert(error.message);
            console.error('Authentifizierungsfehler:', error);
        } finally {
            setIsLoading(false);
        }
    };

    const restaurants = [
        {
            id: 1,
            name: "Café Gemütlich",
            cuisine: "Café & Konditorei",
            rating: 4.5,
            reviews: 128,
            image: "/images/r-it.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: false },
                { id: 2, seats: 2, isBooked: true },
                { id: 3, seats: 4, isBooked: false },
                { id: 4, seats: 4, isBooked: false },
                { id: 5, seats: 6, isBooked: true },
                { id: 6, seats: 8, isBooked: false },
            ],
        },
        {
            id: 2,
            name: "La Cucina",
            cuisine: "Italienisch",
            rating: 4.7,
            reviews: 256,
            image: "/images/r-cui.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: false },
                { id: 2, seats: 2, isBooked: false },
                { id: 3, seats: 4, isBooked: true },
                { id: 4, seats: 4, isBooked: false },
                { id: 5, seats: 6, isBooked: false },
                { id: 6, seats: 8, isBooked: true },
            ],
        },
        {
            id: 3,
            name: "Sushi Master",
            cuisine: "Japanisch",
            rating: 4.6,
            reviews: 189,
            image: "/images/r-sushi2.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: true },
                { id: 2, seats: 2, isBooked: false },
                { id: 3, seats: 4, isBooked: false },
                { id: 4, seats: 4, isBooked: true },
                { id: 5, seats: 6, isBooked: false },
                { id: 6, seats: 8, isBooked: false },
            ],
        },
        {
            id: 4,
            name: "Damaskino",
            cuisine: "Syrian",
            rating: 4.9,
            reviews: 367,
            image: "/images/syr2.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: false },
                { id: 2, seats: 2, isBooked: true },
                { id: 3, seats: 4, isBooked: false },
                { id: 4, seats: 4, isBooked: false },
                { id: 5, seats: 6, isBooked: true },
                { id: 6, seats: 8, isBooked: false },
            ],
        },
        {
            id: 5,
            name: "Taj Mahal",
            cuisine: "Indisch",
            rating: 4.8,
            reviews: 234,
            image: "/images/r-taj.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: true },
                { id: 2, seats: 2, isBooked: false },
                { id: 3, seats: 4, isBooked: false },
                { id: 4, seats: 4, isBooked: true },
                { id: 5, seats: 6, isBooked: false },
                { id: 6, seats: 8, isBooked: true },
            ],
        },
        {
            id: 6,
            name: "Zum Goldenen Hirsch",
            cuisine: "Deutsch",
            rating: 4.3,
            reviews: 145,
            image: "/images/r-hirsch.jpg",
            tables: [
                { id: 1, seats: 2, isBooked: false },
                { id: 2, seats: 2, isBooked: true },
                { id: 3, seats: 4, isBooked: false },
                { id: 4, seats: 4, isBooked: true },
                { id: 5, seats: 6, isBooked: false },
                { id: 6, seats: 8, isBooked: false },
            ],
        },
    ];

    const handleRestaurantSelect = (restaurantId) => {
        setSelectedRestaurant(restaurants.find((r) => r.id === restaurantId));
        setReservationModal(true);
        setSelectedTable(null);
    };

    const handleTableSelect = (tableId) => {
        setSelectedTable(tableId);
    };

    const handleReservation = (e) => {
        e.preventDefault();
        if (!selectedTable) {
            alert("Bitte wählen Sie einen Tisch aus.");
            return;
        }
        alert(
            "Reservierung erfolgreich! Sie erhalten in Kürze eine Bestätigungs-E-Mail."
        );
        setReservationModal(false);
        setSelectedRestaurant(null);
        setSelectedTable(null);
        setName("");
        setEmail("");
        setPhone("");
    };

    return (
        <div className="min-h-screen bg-[#f5f1e9]">
            <nav className="bg-[#2c1810] p-4">
                <div className="container mx-auto flex justify-between items-center">
                    <h1
                        onClick={() => router.push("/")}
                        className="text-3xl font-bold text-[#e6b17e] cursor-pointer hover:text-[#f5f1e9]"
                    >
                        Restaurant Finder
                    </h1>

                    <div className="hidden md:flex space-x-6">
                        <a
                            href="#restaurants"
                            className="text-[#e6b17e] hover:text-[#f5f1e9]"
                        >
                            Restaurants
                        </a>
                        <a href="#about" className="text-[#e6b17e] hover:text-[#f5f1e9]">
                            Über uns
                        </a>
                        <a href="#contact" className="text-[#e6b17e] hover:text-[#f5f1e9]">
                            Kontakt
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

            {isMenuOpen && (
                <div className="md:hidden bg-[#2c1810] p-4">
                    <div className="flex flex-col space-y-4">
                        <a
                            href="#restaurants"
                            className="text-[#e6b17e] hover:text-[#f5f1e9]"
                        >
                            Restaurants
                        </a>
                        <a href="#about" className="text-[#e6b17e] hover:text-[#f5f1e9]">
                            Über uns
                        </a>
                        <a href="#contact" className="text-[#e6b17e] hover:text-[#f5f1e9]">
                            Kontakt
                        </a>
                    </div>
                </div>
            )}

            <header id="restaurants" className="text-center py-20">
                <h2 className="text-4xl font-playfair text-[#2c1810] mb-4">
                    Finden Sie Ihr perfektes Restaurant
                </h2>
                <p className="text-[#5c3d2e] mb-8">
                    Entdecken Sie die besten Restaurants in Ihrer Nähe und reservieren Sie
                    direkt online
                </p>
            </header>

            <section className="container mx-auto py-16 px-4">
                <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                    Unsere Partnerrestaurants
                </h2>

                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                    {restaurants.map((restaurant) => (
                        <div
                            key={restaurant.id}
                            className="bg-white rounded-lg shadow-lg overflow-hidden"
                        >
                            <img
                                src={restaurant.image}
                                alt={restaurant.name}
                                className="w-full h-48 object-cover"
                            />
                            <div className="p-6">
                                <h3 className="text-xl font-playfair text-[#2c1810] mb-2">
                                    {restaurant.name}
                                </h3>
                                <p className="text-[#5c3d2e] mb-4">{restaurant.cuisine}</p>
                                <div className="flex items-center mb-4">
                                    <i className="fas fa-star text-yellow-400 mr-1"></i>
                                    <span>
                                        {restaurant.rating} ({restaurant.reviews} Bewertungen)
                                    </span>
                                </div>
                                <button
                                    onClick={() => handleRestaurantSelect(restaurant.id)}
                                    className="w-full bg-[#2c1810] text-white py-2 rounded hover:bg-[#3d251c]"
                                >
                                    Tisch reservieren
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </section>

            {reservationModal && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4">
                    <div className="bg-white p-8 rounded-lg max-w-md w-full">
                        <h3 className="text-2xl font-playfair mb-4">
                            Reservierung bei {selectedRestaurant?.name}
                        </h3>
                        <form onSubmit={handleReservation}>
                            <div className="grid grid-cols-1 gap-4 mb-4">
                                <input
                                    type="date"
                                    className="w-full p-2 border rounded"
                                    value={selectedDate}
                                    onChange={(e) => setSelectedDate(e.target.value)}
                                    required
                                />
                                <select
                                    className="w-full p-2 border rounded"
                                    value={selectedTime}
                                    onChange={(e) => setSelectedTime(e.target.value)}
                                    required
                                >
                                    <option value="">Uhrzeit wählen</option>
                                    <option value="17:00">17:00</option>
                                    <option value="17:30">17:30</option>
                                    <option value="18:00">18:00</option>
                                    <option value="18:30">18:30</option>
                                    <option value="19:00">19:00</option>
                                    <option value="19:30">19:30</option>
                                    <option value="20:00">20:00</option>
                                </select>
                                <div className="grid grid-cols-2 gap-4">
                                    {selectedRestaurant?.tables
                                        .filter((table) => table.seats >= selectedGuests)
                                        .map((table) => (
                                            <button
                                                key={table.id}
                                                type="button"
                                                disabled={table.isBooked}
                                                onClick={() => handleTableSelect(table.id)}
                                                className={`p-4 border rounded ${table.isBooked
                                                        ? "bg-gray-200 cursor-not-allowed"
                                                        : selectedTable === table.id
                                                            ? "bg-[#2c1810] text-white"
                                                            : "hover:bg-[#f5f1e9]"
                                                    }`}
                                            >
                                                <div className="text-center">
                                                    <i className="fas fa-chair text-xl mb-2"></i>
                                                    <p>Tisch {table.id}</p>
                                                    <p>{table.seats} Plätze</p>
                                                    {table.isBooked && (
                                                        <p className="text-red-500">Besetzt</p>
                                                    )}
                                                </div>
                                            </button>
                                        ))}
                                </div>
                                <input
                                    type="text"
                                    placeholder="Name"
                                    className="w-full p-2 border rounded"
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                />
                                <input
                                    type="email"
                                    placeholder="E-Mail"
                                    className="w-full p-2 border rounded"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                />
                                <input
                                    type="tel"
                                    placeholder="Telefon"
                                    className="w-full p-2 border rounded"
                                    value={phone}
                                    onChange={(e) => setPhone(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="flex justify-end gap-4">
                                <button
                                    type="button"
                                    onClick={() => setReservationModal(false)}
                                    className="px-4 py-2 text-gray-600 hover:bg-gray-100 rounded"
                                >
                                    Abbrechen
                                </button>
                                <button
                                    type="submit"
                                    className="px-4 py-2 bg-[#2c1810] text-white rounded hover:bg-[#3d251c]"
                                >
                                    Reservieren
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

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
            <section id="about" className="bg-[#2c1810] text-[#f5f1e9] py-16 px-4">
                <div className="container mx-auto">
                    <h2 className="text-3xl font-playfair text-center mb-8">Über uns</h2>
                    <p className="max-w-2xl mx-auto text-center">
                        Wir sind leidenschaftliche Gastronomen, die das Ziel haben, das
                        Reservierungserlebnis so einfach und angenehm wie möglich zu machen.
                    </p>
                </div>
            </section>

            <section id="contact" className="container mx-auto py-16 px-4">
                <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                    Kontakt
                </h2>
                <div className="max-w-md mx-auto">
                    <div className="text-center text-[#5c3d2e]">
                        <p className="mb-2">
                            <i className="fas fa-map-marker-alt mr-2"></i>Hauptstraße 123,
                            12345 Stadt
                        </p>
                        <p className="mb-2">
                            <i className="fas fa-phone mr-2"></i>+49 123 456789
                        </p>
                        <p className="mb-2">
                            <i className="fas fa-envelope mr-2"></i>info@restaurant-finder.de
                        </p>
                    </div>
                </div>
            </section>

            <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
                <div className="container mx-auto text-center">
                    <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
                </div>
            </footer>
        </div>
    );
}

export default MainComponent;
