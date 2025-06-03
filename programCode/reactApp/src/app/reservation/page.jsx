// ────────────────────────────────────────────────────────────
// Imports
// ────────────────────────────────────────────────────────────
"use client";
import React from "react";
import { useState, useEffect } from "react";
import { useRouter } from 'next/navigation';
import { useAuth } from '@/app/context/AuthContext';


// ────────────────────────────────────────────────────────────
// Main Component
// ────────────────────────────────────────────────────────────
function MainComponent() {
    // ────────────────────────────────────────────────────────────
    // State Management
    // ────────────────────────────────────────────────────────────
    const [authModalOpen, setAuthModalOpen] = useState(false);
    /** @type {'login' | 'register'} */
    const [authMode, setAuthMode] = useState("login");
    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    const [selectedDate, setSelectedDate] = useState('');
    const [selectedTime, setSelectedTime] = useState('');
    const [restaurants, setRestaurants] = useState([]);
    const [startTime, setStartTime] = useState('');
    const [endTime, setEndTime] = useState('');
    //const [selectedGuests, setSelectedGuests] = useState(2);
    const [selectedRestaurant, setSelectedRestaurant] = useState(null);
    const [reservationModal, setReservationModal] = useState(false);
    const [selectedTable, setSelectedTable] = useState(null);
    const [reservedTables, setReservedTables] = useState([]);
    const [errorMessage, setErrorMessage] = useState(null);

    //const [name, setName] = useState('');
    //const [email, setEmail] = useState('');
    //const [phone, setPhone] = useState('');

    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";


    // ────────────────────────────────────────────────────────────
    // Effects
    // ────────────────────────────────────────────────────────────

    // Authentifizierungslogik
    useEffect(() => {
        let isMounted = true;

        const storedUser = localStorage.getItem('authUser');
        if (storedUser && isMounted) setUser(JSON.parse(storedUser));

        (async () => {
            try {
                const res = await fetch(`${API_URL}/api/User`, {
                    credentials: 'include',                // sends cookies :contentReference[oaicite:2]{index=2}
                    headers: { Accept: 'application/json' },
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
            localStorage.setItem('authState', JSON.stringify(user));
        }
    }, [user]);

    // Ruft Restaurants ab 
    useEffect(() => {
        const fetchRestaurants = async () => {
            try {
                const response = await fetch(`${API_URL}/api/Restaurant`);
                if (!response.ok) throw new Error("Fehler beim Laden der Restaurants");
                const data = await response.json();
                setRestaurants(data);
                console.log(data);
            } catch (error) {
                console.error("Fehler beim Laden der Restaurants:", error);
                alert("Die Restaurants konnten nicht geladen werden.");
            }
        };

        fetchRestaurants();
    }, []);

    // Ruft schon besetzte Tische ab
    useEffect(() => {
        const fetchReservations = async () => {
            if (!selectedDate || !startTime || !endTime || !selectedRestaurant) return;

            try {
                const formattedStartTime = `${selectedDate}T${startTime}:00Z`;
                const formattedEndTime = `${selectedDate}T${endTime}:00Z`;

                const response = await fetch(
                    `${API_URL}/api/Reservation/Public?RestaurantId=${selectedRestaurant.restaurantId}&StartTime=${encodeURIComponent(formattedStartTime)}&EndTime=${encodeURIComponent(formattedEndTime)}`,
                    {
                        method: "GET",
                        headers: {
                            Accept: "*/*",
                        },
                    }
                );

                if (!response.ok) throw new Error("Fehler beim Laden der Reservierungen");
                const data = await response.json();
                setReservedTables(data); // Reservierungen im State speichern
            } catch (error) {
                console.error("Fehler beim Laden der Reservierungen:", error);
            }
        };


        fetchReservations();
    }, [selectedDate, startTime, endTime, selectedRestaurant]);




    // ────────────────────────────────────────────────────────────
    // Event Handlers
    // ────────────────────────────────────────────────────────────
    //Logout
    const handleLogout = async () => {
        try {
            await fetch(`${API_URL}/api/Auth/logout`, { method: 'POST', credentials: 'include' });
            setUser(null);
            localStorage.removeItem('authUser');
        } catch (err) {
            console.error('Fehler beim Logout:', err);
        }
    };

    //Authentifizierung
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
    //const restaurants = [
    //    {
    //        id: 1,
    //        name: "Café Gemütlich",
    //        cuisine: "Café & Konditorei",
    //        rating: 4.5,
    //        reviews: 128,
    //        image: "/images/r-it.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: false },
    //            { id: 2, seats: 2, isBooked: true },
    //            { id: 3, seats: 4, isBooked: false },
    //            { id: 4, seats: 4, isBooked: false },
    //            { id: 5, seats: 6, isBooked: true },
    //            { id: 6, seats: 8, isBooked: false },
    //        ],
    //    },
    //    {
    //        id: 2,
    //        name: "La Cucina",
    //        cuisine: "Italienisch",
    //        rating: 4.7,
    //        reviews: 256,
    //        image: "/images/r-cui.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: false },
    //            { id: 2, seats: 2, isBooked: false },
    //            { id: 3, seats: 4, isBooked: true },
    //            { id: 4, seats: 4, isBooked: false },
    //            { id: 5, seats: 6, isBooked: false },
    //            { id: 6, seats: 8, isBooked: true },
    //        ],
    //    },
    //    {
    //        id: 3,
    //        name: "Sushi Master",
    //        cuisine: "Japanisch",
    //        rating: 4.6,
    //        reviews: 189,
    //        image: "/images/r-sushi2.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: true },
    //            { id: 2, seats: 2, isBooked: false },
    //            { id: 3, seats: 4, isBooked: false },
    //            { id: 4, seats: 4, isBooked: true },
    //            { id: 5, seats: 6, isBooked: false },
    //            { id: 6, seats: 8, isBooked: false },
    //        ],
    //    },
    //    {
    //        id: 4,
    //        name: "Damaskino",
    //        cuisine: "Syrian",
    //        rating: 4.9,
    //        reviews: 367,
    //        image: "/images/syr2.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: false },
    //            { id: 2, seats: 2, isBooked: true },
    //            { id: 3, seats: 4, isBooked: false },
    //            { id: 4, seats: 4, isBooked: false },
    //            { id: 5, seats: 6, isBooked: true },
    //            { id: 6, seats: 8, isBooked: false },
    //        ],
    //    },
    //    {
    //        id: 5,
    //        name: "Taj Mahal",
    //        cuisine: "Indisch",
    //        rating: 4.8,
    //        reviews: 234,
    //        image: "/images/r-taj.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: true },
    //            { id: 2, seats: 2, isBooked: false },
    //            { id: 3, seats: 4, isBooked: false },
    //            { id: 4, seats: 4, isBooked: true },
    //            { id: 5, seats: 6, isBooked: false },
    //            { id: 6, seats: 8, isBooked: true },
    //        ],
    //    },
    //    {
    //        id: 6,
    //        name: "Zum Goldenen Hirsch",
    //        cuisine: "Deutsch",
    //        rating: 4.3,
    //        reviews: 145,
    //        image: "/images/r-hirsch.jpg",
    //        tables: [
    //            { id: 1, seats: 2, isBooked: false },
    //            { id: 2, seats: 2, isBooked: true },
    //            { id: 3, seats: 4, isBooked: false },
    //            { id: 4, seats: 4, isBooked: true },
    //            { id: 5, seats: 6, isBooked: false },
    //            { id: 6, seats: 8, isBooked: false },
    //        ],
    //    },
    //];


    // Restaurant Auswahl
    const handleRestaurantSelect = (restaurantId) => {
        const restaurant = restaurants.find((r) => r.restaurantId === restaurantId);
        setSelectedRestaurant(restaurant);
        setReservationModal(true);
        setSelectedTable(null);
    };


    // Tisch Auswahl
    const handleTableSelect = (tableId) => {
        setSelectedTable(tableId);
    };

    // Reservierungslogik
    const handleReservation = async (e) => {
        e.preventDefault();

        if (!selectedTable) {
            alert("Bitte wählen Sie einen Tisch aus.");
            return;
        }

        if (!user) {
            alert("Bitte loggen Sie sich ein, um eine Reservierung vorzunehmen.");
            return;
        }

        const formattedStartTime = `${selectedDate}T${startTime}:00Z`;
        const formattedEndTime = `${selectedDate}T${endTime}:00Z`;

        try {
            const response = await fetch(`${API_URL}/api/Reservation/${selectedTable}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${user.token}`, // Token des Benutzers
                },
                body: JSON.stringify({
                    startTime: formattedStartTime,
                    endTime: formattedEndTime,
                }),
            });

            if (!response.ok) {
                const { message } = await response.json();
                throw new Error(message || "Reservierung fehlgeschlagen.");
            }

            alert("Reservierung erfolgreich! Sie erhalten in Kürze eine Bestätigungs-E-Mail.");
            setReservationModal(false);
            setSelectedRestaurant(null);
            setSelectedTable(null);
            setErrorMessage(null);
        } catch (error) {
            console.error("Fehler bei der Reservierung:", error);
            alert(error.message || "Ein Fehler ist aufgetreten.");
            setErrorMessage(error.message || "Ein Fehler ist aufgetreten.");
        }
    };


    // ────────────────────────────────────────────────────────────
    // JSX Rendering
    // ────────────────────────────────────────────────────────────
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
                                <span
                                    onClick={() => router.push("/my-reservations")}
                                    className="text-[#e6b17e] cursor-pointer hover:text-[#f5f1e9]"
                                >
                                    Willkommen, {user.user.firstName}
                                </span>
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

            {/* Header */}
            <header id="restaurants" className="text-center py-20">
                <h2 className="text-4xl font-playfair text-[#2c1810] mb-4">
                    Finden Sie Ihr perfektes Restaurant
                </h2>
                <p className="text-[#5c3d2e] mb-8">
                    Entdecken Sie die besten Restaurants in Ihrer Nähe und reservieren Sie
                    direkt online
                </p>
            </header>

            {/* Restaurant-Liste */}
            <section className="container mx-auto py-16 px-4">
                <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                    Unsere Partnerrestaurants
                </h2>

                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                    {restaurants.map((restaurant) => (
                        <div
                            key={restaurant.restaurantId}
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
                                <p className="text-[#5c3d2e] mb-4">{restaurant.address}</p>
                                <p className="text-[#5c3d2e] mb-4">{restaurant.openingHours}</p>
                                <div className="flex items-center mb-4">
                                    <i className="fas fa-star text-yellow-400 mr-1"></i>
                                    <span>
                                        {restaurant.rating} ({restaurant.reviews} Bewertungen)
                                    </span>
                                </div>
                                <button
                                    onClick={() => handleRestaurantSelect(restaurant.restaurantId)}
                                    className="w-full bg-[#2c1810] text-white py-2 rounded hover:bg-[#3d251c]"
                                >
                                    Tisch reservieren
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </section>

            {/* Reservierungsmodal */}
            {reservationModal && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4">
                    <div className="bg-white p-8 rounded-lg max-w-md w-full">
                        <h3 className="text-2xl font-playfair mb-4">
                            Reservierung bei {selectedRestaurant?.name}
                        </h3>
                        {errorMessage && (
                            <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">
                                <strong className="font-bold">Fehler: </strong>
                                <span className="block sm:inline">{errorMessage}</span>
                                <button
                                    onClick={() => setErrorMessage(null)}
                                    className="absolute top-0 bottom-0 right-0 px-4 py-3"
                                >
                                    <span className="text-red-500">&times;</span>
                                </button>
                            </div>
                        )}

                        <form onSubmit={handleReservation}>
                            <div className="grid grid-cols-1 gap-4 mb-4">
                                <input
                                    type="date"
                                    className="w-full p-2 border rounded"
                                    value={selectedDate}
                                    onChange={(e) => setSelectedDate(e.target.value)}
                                    required
                                />
                                {/* Von Uhrzeit */}
                                <div className="flex gap-4">
                                    <div className="flex-1">
                                        <label htmlFor="startTime" className="block text-sm text-[#5c3d2e] mb-1">
                                            Von:
                                        </label>
                                        <input
                                            id="startTime"
                                            type="time"
                                            className="w-full p-2 border rounded"
                                            value={startTime}
                                            onChange={(e) => setStartTime(e.target.value)}
                                            required
                                        />
                                    </div>
                                    {/* Bis Uhrzeit */}
                                    <div className="flex-1">
                                        <label htmlFor="endTime" className="block text-sm text-[#5c3d2e] mb-1">
                                            Bis:
                                        </label>
                                        <input
                                            id="endTime"
                                            type="time"
                                            className="w-full p-2 border rounded"
                                            value={endTime}
                                            onChange={(e) => setEndTime(e.target.value)}
                                            required
                                        />
                                    </div>
                                </div>

                                <div className="grid grid-cols-2 gap-4">
                                    {selectedRestaurant?.tables.length > 0 ? (
                                        selectedRestaurant.tables.map((table) => {
                                            const isReserved = reservedTables.some(
                                                (reservation) => reservation.tableId === table.tableId
                                            ); // Prüfen, ob der Tisch reserviert ist
                                            return (
                                                <button
                                                    key={table.tableId}
                                                    type="button"
                                                    disabled={!selectedDate || !startTime || !endTime || isReserved} // Deaktivieren, wenn reserviert
                                                    onClick={() => handleTableSelect(table.tableId)}
                                                    className={`p-4 border rounded ${selectedTable === table.tableId
                                                            ? "bg-[#2c1810] text-white" // Hervorhebung für den ausgewählten Tisch
                                                            : isReserved
                                                                ? "bg-gray-200 cursor-not-allowed" // Deaktivierter Zustand für reservierte Tische
                                                                : "hover:bg-[#f5f1e9]" // Standard-Hover-Effekt
                                                        }`}
                                                >
                                                    <div className="text-center">
                                                        <i className="fas fa-chair text-xl mb-2"></i>
                                                        <p>Tisch {table.tableNr}</p>
                                                        <p>{table.capacity} Plätze</p>
                                                        <p>Bereich: {table.area}</p>
                                                        {isReserved && <p className="text-red-500">Reserviert</p>}
                                                    </div>
                                                </button>
                                            );
                                        })
                                    ) : (
                                        <p>Keine Tische verfügbar.</p>
                                    )}
                                </div>




                                {/*<div className="grid grid-cols-2 gap-4">*/}
                                {/*    {selectedRestaurant?.tables*/}
                                {/*        //.filter((table) => table.seats >= selectedGuests)*/}
                                {/*        .map((table) => (*/}
                                {/*            <button*/}
                                {/*                key={table.tableId}*/}
                                {/*                type="button"*/}
                                {/*                disabled={*/}
                                {/*                    !selectedDate || !selectedTime //|| table.isBooked*/}
                                {/*                }*/}
                                {/*                onClick={() => handleTableSelect(table.tableId)}*/}
                                {/*                className={`p-4 border rounded ${//table.isBooked*/}
                                {/*                    //? "bg-gray-200 cursor-not-allowed"*/}
                                {/*                    //:*/}
                                {/*    selectedTable === table.tableId*/}
                                {/*                        ? "bg-[#2c1810] text-white"*/}
                                {/*                        : "hover:bg-[#f5f1e9]"*/}
                                {/*                    }`}*/}
                                {/*            >*/}
                                {/*                <div className="text-center">*/}
                                {/*                    <i className="fas fa-chair text-xl mb-2"></i>*/}
                                {/*                    <p>Tisch {table.id}</p>*/}
                                {/*                    <p>{table.seats} Plätze</p>*/}
                                {/*                    {table.isBooked && (*/}
                                {/*                        <p className="text-red-500">Besetzt</p>*/}
                                {/*                    )}*/}
                                {/*                </div>*/}
                                {/*            </button>*/}
                                {/*        ))}*/}
                                {/*</div>*/}




                                {/*<input*/}
                                {/*    type="text"*/}
                                {/*    placeholder="Name"*/}
                                {/*    className="w-full p-2 border rounded"*/}
                                {/*    value={name}*/}
                                {/*    onChange={(e) => setName(e.target.value)}*/}
                                {/*    required*/}
                                {/*/>*/}
                                {/*<input*/}
                                {/*    type="email"*/}
                                {/*    placeholder="E-Mail"*/}
                                {/*    className="w-full p-2 border rounded"*/}
                                {/*    value={email}*/}
                                {/*    onChange={(e) => setEmail(e.target.value)}*/}
                                {/*    required*/}
                                {/*/>*/}
                                {/*<input*/}
                                {/*    type="tel"*/}
                                {/*    placeholder="Telefon"*/}
                                {/*    className="w-full p-2 border rounded"*/}
                                {/*    value={phone}*/}
                                {/*    onChange={(e) => setPhone(e.target.value)}*/}
                                {/*    required*/}
                                {/*/>*/}
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

            {/* Über uns */}
            <section id="about" className="bg-[#2c1810] text-[#f5f1e9] py-16 px-4">
                <div className="container mx-auto">
                    <h2 className="text-3xl font-playfair text-center mb-8">Über uns</h2>
                    <p className="max-w-2xl mx-auto text-center">
                        Wir sind leidenschaftliche Gastronomen, die das Ziel haben, das
                        Reservierungserlebnis so einfach und angenehm wie möglich zu machen.
                    </p>
                </div>
            </section>

            {/* Kontakt */}
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

            {/* Footer */}
            <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
                <div className="container mx-auto text-center">
                    <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
                </div>
            </footer>
        </div>
    );
}

export default MainComponent;
