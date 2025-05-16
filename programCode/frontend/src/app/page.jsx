"use client";
import React from "react";

function Homepage() {
  return (
    <div className="min-h-screen bg-[#f5f1e9]">
      <nav className="bg-[#2c1810] p-4">
        <div className="container mx-auto flex justify-between items-center">
          <h1 className="text-[#e6b17e] text-2xl font-playfair">Home</h1>
          <a href="/resowner" className="text-[#e6b17e] hover:text-[#f5f1e9]">
          Restaurant owners form
          </a>
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

      <footer className="bg-[#2c1810] text-[#e6b17e] py-6">
        <div className="container mx-auto text-center">
          <p>© 2025 Restaurant Finder. Alle Rechte vorbehalten.</p>
        </div>
      </footer>
    </div>
  );
}

export default Homepage;
