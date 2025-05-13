"use client";
import React, { useState } from "react";
import { useRouter } from "next/navigation";

function AddRestaurantForm() {
  const [name, setName] = useState("");
  const [cuisine, setCuisine] = useState("");
  const [rating, setRating] = useState("");
  const [image, setImage] = useState("");
  const [message, setMessage] = useState("");
  const router = useRouter();

  const handleSubmit = (e) => {
    e.preventDefault();

    // Hier könnte man das Restaurant z.B. per API an das Backend schicken
    if (!name || !cuisine || !rating || !image) {
      setMessage("Bitte füllen Sie alle Felder aus.");
      return;
    }

    setMessage("Restaurant erfolgreich hinzugefügt!");
    // Nach dem Speichern zurück zur Startseite oder Clear Form
    setName("");
    setCuisine("");
    setRating("");
    setImage("");

    // router.push("/"); // Falls du auf Startseite zurück möchtest
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
        </div>
      </nav>

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
              Küche / Typ
            </label>
            <input
              type="text"
              className="w-full p-2 border rounded"
              value={cuisine}
              onChange={(e) => setCuisine(e.target.value)}
              required
            />
          </div>

          <div className="mb-4">
            <label className="block mb-2 text-[#2c1810] font-semibold">
              Bewertung (1 - 5)
            </label>
            <input
              type="number"
              min="1"
              max="5"
              step="0.1"
              className="w-full p-2 border rounded"
              value={rating}
              onChange={(e) => setRating(e.target.value)}
              required
            />
          </div>

          <div className="mb-4">
            <label className="block mb-2 text-[#2c1810] font-semibold">
              Bild-URL
            </label>
            <input
              type="text"
              className="w-full p-2 border rounded"
              value={image}
              onChange={(e) => setImage(e.target.value)}
              required
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

      <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
        <div className="container mx-auto text-center">
          <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
        </div>
      </footer>
    </div>
  );
}

export default AddRestaurantForm;
