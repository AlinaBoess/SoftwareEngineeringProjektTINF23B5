# 🛠️ Technischer Review

**Datum:** 27.05.2025  
**Dauer:** 30 Min (16:30–17:00)  
**Teilnehmer:** Safae Kartite, Alexander Fleig, Yahya Ezz Edin, Moumen Kheto, Alina Boess, Lukas Scharnweber  
**Moderation:** Alexander Fleig  
**Protokoll:** Lukas Scharnweber  

---

## 1. 🎯 Ziel / Fokus

- Identifikation von Schwachstellen, Sicherheitslücken, Bottlenecks und Logikfehlern im Backend.  
- Analyse der Kernkomponenten: Authentifizierung, Benutzerverwaltung, Feedback-System, Unit Tests.  

---

## 2. 🔍 Umfang

### Ausgewählte Abschnitte
- **Backend**:  
  - Authentifizierung (Login / Registrierung)  
  - Benutzerverwaltung  
  - Feedback-System für Restaurants  
  - Unit Tests  

**Ausgeschlossen:** Frontend (noch in Entwicklung)

---

## 3. 🧪 Methodik

- Pauschaler Walkthrough der Projektstruktur.  
- Detaillierte Code-Reviews pro Baustein anhand geöffneter Files in Visual Studio.  
- Diskussion zu Best Practices, Sicherheit und Performance.

---

## 4. 📊 Übersicht der Kernergebnisse

| Bereich            | Bewertung | Anmerkungen                                                     |
|--------------------|-----------|------------------------------------------------------------------|
| Codequalität       | 👍 Gut     | Klare Struktur, sinnvolle Layeraufteilung                        |
| Performance (Backend) | 👍 Gut     | Effiziente Datenbankabfragen (EF Core), keine offensichtlichen Bottlenecks |
| Security           | 👍 Gut     | Passwort-Hashing vorhanden, Token-basierte Authentifizierung     |
| Exception Handling | 👍 Gut     | Error Handling, Logging integriert                               |
| Namenskonventionen | ❌ Verbessern | Inkonsistente Bezeichnungen (z. B. `ReservationSystem.cs`)         |
| Code Cleanup       | ❌ Verbessern | Auskommentierter Code, ungenutzte Services / Attribute           |

---

## 5. 🔍 Detaillierte Findings & Empfehlungen

### 5.1 Authentifizierung & Registrierung

**`UserService.cs`**
- Registrierung sollte in `AuthService` ausgelagert werden (bessere Strukturierung).
- Duplicate Email Handling: klare Fehlermeldung bei doppelter E-Mail (z. B. `EmailAlreadyExistsException`).
- Empfehlung: JWT-Token nach erfolgreicher Registrierung im Response-Body zurückgeben.

**Sicherheit**
- Passwörter werden mit bcrypt gehasht.
- Tokenbasierte Authentifizierung mit JWT vorhanden.

---

### 5.2 Benutzerverwaltung

**`User.cs`**
- Unbenutzte Attribute (z. B. `AdminActions`) entfernen.

**`AdminService.cs`**
- Löschen, da nicht verwendet (Dead Code).

**Namenskonvention**
- Eindeutige Suffixe verwenden: `*Service`, `*Controller`, `*Model`.

---

### 5.3 ReservationSystem

**`ReservationSystem.cs`**
- Umbenennen in `ReservationService`.

**`ReservationSystemController.cs`**
- Geschäftslogik in `ReservationService` auslagern.
- Auskommentierten Code und TODOs entfernen.

---

### 5.4 FeedbackService & Restaurant-Module

**`FeedbackService.cs`**
- Nutzer sollen eigene Kommentare selbst löschen dürfen, nicht nur Admins.

**`RestaurantOwnerService.cs`**
- Umbenennen in `RestaurantService`.
- Enthaltene Suchfunktion für Restaurants korrekt implementieren.

---

### 5.5 Unit Tests

- Tests für Fehlerfälle ergänzen (z. B. doppelte Registrierung, ungültiger Token).
- Testdaten über Fixtures / Factories generieren, um Redundanzen zu vermeiden.

---

## 6. ✅ Zusammenfassung

### Stärken
- Solide Authentifizierungsarchitektur
- Gute Exception-Handling-Struktur
- Ordentliche Performance

### Schwächen
- Unnötiger / auskommentierter Code
- Uneinheitliche Namenskonventionen
- Teilweise unklare Verantwortlichkeitsverteilung

---

## 7. 📌 Nächste Schritte

- [ ] Nicht genutzten Code entfernen (`AdminService`, auskommentierte Blöcke, ungenutzte Attribute).
- [ ] Namenskonventionen vereinheitlichen (`*Service`, `*Controller`, `*Model`).
- [ ] Registrierungsvorgang in `AuthService` zentralisieren.
- [ ] Unit Tests für Fehlerfälle erweitern.
- [ ] Restaurant-Suchfunktion in `RestaurantService` integrieren.
- [ ] Feedback-Löschung durch Nutzer selbst ermöglichen.

---

🔗 [Zum Repository](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5)
