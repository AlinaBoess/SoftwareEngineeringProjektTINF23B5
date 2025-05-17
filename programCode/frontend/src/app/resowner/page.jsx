"use client";
import { useRouter } from "next/navigation";
import { useState, useEffect } from "react";

function AddRestaurantForm() {
  const [name, setName] = useState("");
  const [cuisine, setCuisine] = useState("");
  const [rating, setRating] = useState("");
  const [image, setImage] = useState("");
  const [message, setMessage] = useState("");
  const [authModalOpen, setAuthModalOpen] = useState(false);
  const [authMode, setAuthMode] = useState("login");
  const [user, setUser] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

    const router = useRouter();
    const API_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7038";
    useEffect(() => {
        let isMounted = true;

        // Immediately set user from localStorage if available
        const storedUser = localStorage.getItem('authUser');
        if (storedUser && isMounted) {
            setUser(JSON.parse(storedUser));
        }

        // Then verify with server
        const checkAuth = async () => {
            try {
                const res = await fetch(`${API_URL}/api/User`, {
                    credentials: "include",
                    headers: { 'Accept': 'application/json' }
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
                if (isMounted) {
                    console.error("Auth check failed:", err);
                    setUser(null);
                    localStorage.removeItem('authUser');
                }
            }
        };

        checkAuth();

        return () => {
            isMounted = false;
        };
    }, []);
    useEffect(() => {
        const syncAuthState = () => {
            const storedUser = localStorage.getItem('authUser');
            setUser(storedUser ? JSON.parse(storedUser) : null);
        };

        window.addEventListener('storage', syncAuthState);
        return () => window.removeEventListener('storage', syncAuthState);
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
            localStorage.setItem('authUser', JSON.stringify(data)); //Added to maintain login data
            setAuthModalOpen(false);

        } catch (error) {
            alert(error.message);
            console.error('Authentifizierungsfehler:', error);
        } finally {
            setIsLoading(false);
        }
    };


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
    useEffect(() => {
        const syncAuthState = (e) => {
            if (e.key === 'authState') {
                setUser(JSON.parse(e.newValue));
            }
        };

        window.addEventListener('storage', syncAuthState);
        return () => window.removeEventListener('storage', syncAuthState);
    }, []);

    // Update when user changes
    useEffect(() => {
        if (typeof window !== 'undefined') {
            window.localStorage.setItem('authState', JSON.stringify(user));
        }
    }, [user]);
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
              <div className="flex items-center gap-4">
                  {user ? (
                      <>
                          <span className="text-[#e6b17e]">Willkommen, {user.name}</span>
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
          </nav>
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
