"use client";
import React, { useEffect, useState } from "react";

export default function MyReservations() {
    const [reservations, setReservations] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";

    useEffect(() => {
        const fetchReservations = async () => {
            try {
                const response = await fetch(`${API_URL}/api/Reservation/User`, {
                    credentials: "include",
                });
                if (!response.ok) throw new Error("Fehler beim Laden der Reservierungen");
                const data = await response.json();
                setReservations(data);
            } catch (error) {
                console.error("Fehler beim Laden der Reservierungen:", error);
            } finally {
                setIsLoading(false);
            }
        };

        fetchReservations();
    }, []);

    const handleDelete = async (reservationId) => {
        if (!confirm("Möchten Sie diese Reservierung wirklich löschen?")) return;

        try {
            const response = await fetch(`${API_URL}/api/Reservation/${reservationId}`, {
                method: "DELETE",
                credentials: "include",
            });
            if (!response.ok) throw new Error("Fehler beim Löschen der Reservierung");
            setReservations((prev) =>
                prev.filter((reservation) => reservation.id !== reservationId)
            );
            alert("Reservierung erfolgreich gelöscht.");
        } catch (error) {
            console.error("Fehler beim Löschen der Reservierung:", error);
            alert("Ein Fehler ist aufgetreten.");
        }
    };

    if (isLoading) return <p>Reservierungen werden geladen...</p>;

    return (
        <div className="container mx-auto py-16 px-4">
            <h2 className="text-3xl font-playfair text-center text-[#2c1810] mb-8">
                Meine Reservierungen
            </h2>
            {reservations.length === 0 ? (
                <p className="text-center text-[#5c3d2e]">Keine Reservierungen gefunden.</p>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                    {reservations.map((reservation) => (
                        <div
                            key={reservation.id}
                            className="bg-white rounded-lg shadow-lg p-6"
                        >
                            <h3 className="text-xl font-playfair text-[#2c1810] mb-2">
                                {reservation.restaurantName}
                            </h3>
                            <p className="text-[#5c3d2e] mb-2">
                                Datum: {new Date(reservation.startTime).toLocaleDateString()}
                            </p>
                            <p className="text-[#5c3d2e] mb-2">
                                Zeit: {new Date(reservation.startTime).toLocaleTimeString()} -{" "}
                                {new Date(reservation.endTime).toLocaleTimeString()}
                            </p>
                            <button
                                onClick={() => handleDelete(reservation.id)}
                                className="w-full bg-red-500 text-white py-2 rounded hover:bg-red-600"
                            >
                                Löschen
                            </button>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
