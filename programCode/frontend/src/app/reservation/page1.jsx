"use client";
import React, { useState } from "react";
import { useRouter } from 'next/navigation';
import { useAuth } from './context/AuthContext';

const ReservationPage = () => {
    const { user } = useAuth();
    const router = useRouter();
    const [selectedDate, setSelectedDate] = useState("");
    const [selectedTime, setSelectedTime] = useState("");
    const [selectedGuests, setSelectedGuests] = useState(2);
    const [name, setName] = useState(user?.name || "");
    const [email, setEmail] = useState(user?.email || "");
    const [phone, setPhone] = useState("");
    const handleSubmit = (e) => {
        e.preventDefault();
        if (!user) {
            alert("Bitte loggen Sie sich ein, um eine Reservierung vorzunehmen.");
            return;
        }

        alert(`Reservierung bestätigt für ${selectedDate} um ${selectedTime} für ${selectedGuests} Personen.`);
        router.push("/confirmation");
    };

    return (
        <div className="min-h-screen bg-[#f5f1e9] p-4">
            <div className="max-w-4xl mx-auto bg-white rounded-lg shadow-md p-8">
                <h1 className="text-3xl font-playfair text-[#2c1810] mb-6">Tisch reservieren</h1>

                <form onSubmit={handleSubmit} className="space-y-6">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                        <div>
                            <label className="block text-[#5c3d2e] mb-2">Datum</label>
                            <input
                                type="date"
                                className="w-full p-2 border rounded"
                                value={selectedDate}
                                onChange={(e) => setSelectedDate(e.target.value)}
                                required
                            />
                        </div>

                        <div>
                            <label className="block text-[#5c3d2e] mb-2">Uhrzeit</label>
                            <select
                                className="w-full p-2 border rounded"
                                value={selectedTime}
                                onChange={(e) => setSelectedTime(e.target.value)}
                                required
                            >
                                <option value="">Uhrzeit wählen</option>
                                {["17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00"].map(time => (
                                    <option key={time} value={time}>{time}</option>
                                ))}
                            </select>
                        </div>

                        <div>
                            <label className="block text-[#5c3d2e] mb-2">Anzahl Personen</label>
                            <select
                                className="w-full p-2 border rounded"
                                value={selectedGuests}
                                onChange={(e) => setSelectedGuests(e.target.value)}
                            >
                                {[1, 2, 3, 4, 5, 6, 7, 8].map(num => (
                                    <option key={num} value={num}>{num} {num === 1 ? 'Person' : 'Personen'}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div className="space-y-4">
                        <h2 className="text-xl font-playfair text-[#2c1810]">Ihre Kontaktdaten</h2>

                        <div>
                            <label className="block text-[#5c3d2e] mb-2">Name</label>
                            <input
                                type="text"
                                className="w-full p-2 border rounded"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                required
                                disabled={!!user}
                            />
                        </div>

                        <div>
                            <label className="block text-[#5c3d2e] mb-2">E-Mail</label>
                            <input
                                type="email"
                                className="w-full p-2 border rounded"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                                disabled={!!user}
                            />
                        </div>

                        <div>
                            <label className="block text-[#5c3d2e] mb-2">Telefon</label>
                            <input
                                type="tel"
                                className="w-full p-2 border rounded"
                                value={phone}
                                onChange={(e) => setPhone(e.target.value)}
                                required
                            />
                        </div>
                    </div>

                    <div className="pt-4">
                        <button
                            type="submit"
                            className="px-6 py-3 bg-[#2c1810] text-white rounded hover:bg-[#3d251c] w-full md:w-auto"
                        >
                            Reservierung bestätigen
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ReservationPage;