"use client";
import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import "../globals.css";

function MyReservations() {
    const [reservations, setReservations] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";
    const [user, setUser] = useState(null);
    const router = useRouter();

    useEffect(() => {
        const fetchReservations = async () => {
            try {
                const stored = localStorage.getItem("authState");
                const token = stored ? JSON.parse(stored)?.token : null;
                if (!token) {
                    throw new Error("Kein Authentifizierungstoken gefunden");
                }
                const response = await fetch(`${API_URL}/api/Reservation/User`, {
                    method: "GET",
                    headers: {
                        "Authorization": `Bearer ${token}`,
                        "Content-Type": "application/json",
                    },
                    credentials: "include",
                });
                if (!response.ok) {
                    const errorData = await response.text();
                    console.error("API Fehler:", response.status, errorData);
                    throw new Error(`Fehler beim Laden der Reservierungen: ${response.status}`);
                }
                const data = await response.json();
                setReservations(data);
            } catch (error) {
                console.error("Fehler beim Laden der Reservierungen:", error);
                setError(error.message);
            } finally {
                setIsLoading(false);
            }
        };

        fetchReservations();
    }, []);

    useEffect(() => {
        const syncAuth = async () => {
            const stored = localStorage.getItem("authState");
            console.log("Raw authUser from localStorage:", stored);
            setUser(stored ? JSON.parse(stored) : null);
            //try {
            //    const data = await res.json();   // user DTO returned by the backend
            //    console.log("User DTO from backend:", data);
            //    setUser(data);
            //    localStorage.setItem('authUser', JSON.stringify(data));
            //    setAuthModalOpen(false);
            //} catch { console, error("error ", data) }
        };
        window.addEventListener("storage", syncAuth);
        syncAuth();
        return () => window.removeEventListener("storage", syncAuth);
    }, []);

    useEffect(() => {
        if (typeof window !== "undefined") {
            localStorage.setItem("authUser", JSON.stringify(user));
        }
    }, [user]);

    const handleDelete = async (reservationId) => {
    if (!confirm("Möchten Sie diese Reservierung wirklich löschen?")) return;

    try {
        const storedUser = JSON.parse(localStorage.getItem("authState"));
        const token = storedUser?.token;
        
        if (!token) {
            throw new Error("Kein Authentifizierungstoken gefunden");
        }

        const response = await fetch(`${API_URL}/api/Reservation/${reservationId}`, {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            credentials: "include",
        });

        if (!response.ok) {
            const errorData = await response.text();
            throw new Error(`Fehler beim Löschen der Reservierung: ${errorData}`);
        }

        setReservations((prev) =>
            prev.filter((reservation) => reservation.reservationId !== reservationId)
        );
        alert("Reservierung erfolgreich gelöscht.");
    } catch (error) {
        console.error("Fehler beim Löschen der Reservierung:", error);
        alert(error.message || "Ein Fehler ist aufgetreten.");
    }
};

    if (isLoading) return <p>Reservierungen werden geladen...</p>;
    if (error) return <p className="text-center text-red-500">{error}</p>;

return (
    <div className="min-h-screen bg-[#f5f1e9] py-16 px-4">
        <div className="bg-white p-8 rounded-lg max-w-4xl w-full mx-auto shadow-md">
            <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                Meine Reservierungen
            </h2>
            {reservations.length === 0 ? (
                <p className="text-center text-[#5c3d2e]">
                    Keine Reservierungen gefunden.
                </p>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                    {reservations.map((reservation) => (
                        <div
                            key={reservation.reservationId}
                            className="bg-[#f5f1e9] border border-[#2c1810] rounded-lg p-6 shadow-sm hover:bg-[#f0e9dd] transition"
                        >
                            <h3 className="text-xl font-playfair text-[#2c1810] mb-2">
                                Restaurant: {reservation.restaurant.name}
                            </h3>
                            <p className="text-[#5c3d2e] mb-2">
                                Adresse: {reservation.restaurant.address}
                            </p>
                            <p className="text-[#5c3d2e] mb-2">
                                Webseite:{" "}
                                <a
                                    href={reservation.restaurant.website}
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="text-[#2c1810] underline hover:text-[#3d251c]"
                                >
                                    {reservation.restaurant.website}
                                </a>
                            </p>
                            <p className="text-[#5c3d2e] mb-2">
                                Tischnummer: {reservation.table.tableNr}
                            </p>
                            <p className="text-[#5c3d2e] mb-2">
                                Kapazität: {reservation.table.capacity} Personen
                            </p>
                            <p className="text-[#5c3d2e] mb-2">
                                Datum: {new Date(reservation.startTime).toLocaleDateString()}
                            </p>
                            <p className="text-[#5c3d2e] mb-4">
                                Zeit: {new Date(reservation.startTime).toLocaleTimeString()} -{" "}
                                {new Date(reservation.endTime).toLocaleTimeString()}
                            </p>
                            <button
                                onClick={() => handleDelete(reservation.reservationId)}
                                className="w-full bg-[#2c1810] text-white py-2 rounded hover:bg-[#3d251c] transition"
                            >
                                Löschen
                            </button>
                        </div>
                    ))}
                </div>
            )}
        </div>
    </div>
);

}
export default MyReservations;