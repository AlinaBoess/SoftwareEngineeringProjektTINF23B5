# Blog #17  
**Willkommen zurück zu unserem Projektblog! Schön, dass ihr wieder dabei seid!**

## Technisches Review

Am 27. Mai 2025 fand ein technisches Review durch ein externes Gruppenmitglied statt. Ziel war es, zentrale Komponenten unseres Backends im Hinblick auf Sicherheit, Performance und Codequalität zu untersuchen. Da sich das Frontend noch in Entwicklung befindet, lag der Fokus auf dem Backend, insbesondere auf der Authentifizierung, Benutzerverwaltung und den Unit Tests.

Das Review verlief insgesamt sehr positiv: Die Codequalität und Sicherheitsaspekte wurden als gut bewertet. Dennoch wurde auch Verbesserungspotenzial identifiziert – insbesondere beim Thema Namenskonventionen und allgemeinem Code-Aufräumen.

---

## **Technical Review**  
**Datum:** 27.05.2025  
**Dauer:** 30 Minuten  
**Zeit:** 16:30 Uhr – 17:00 Uhr

### Teilnehmer
- Safae Kartite (Extern)
- Alexander Fleig
- Yahya Ezz Edin
- Moumen Kheto
- Alina Boess
- Lukas Scharnweber

**Moderation:** Alexander Fleig  
**Notizen:** Lukas Scharnweber  
**Code Review (Intern):** Alexander Fleig, Lukas Scharnweber  
**Code Review (Extern):** Safae Kartite  
**Projektmanagement Review:** Yahya Ezz Edin

---

### Ziel / Fokus
Identifikation von:
- Schwachstellen
- Sicherheitslücken
- Performance-Bottlenecks
- Logikfehlern

### Untersuchte Abschnitte
- Authentifizierung (Backend)
- Benutzerverwaltung (Backend)
- Feedbacksystem
- Unit Tests

**Warum?**  
Diese Komponenten bilden das Fundament der Anwendung und sind entscheidend für Benutzerfreundlichkeit, Sicherheit und Einhaltung rechtlicher Anforderungen bei der Datenverarbeitung.

---

### Review-Kriterien & Ergebnisse

| Kriterium            | Bewertung    |
|----------------------|--------------|
| **Codequalität**     | Gut          |
| **Performance**      | Gut (Backend)|
| **Sicherheit**       | Gut          |
| **Fehlerbehandlung** | Gut          |
| **Namenskonventionen** | Verbesserungswürdig |
| **Code-Struktur / Aufräumen** | Verbesserungswürdig |

---

### Review-Methodik
- Walkthrough & gezielte Codeanalyse
- Fokus auf zentrale Dateien und Services

### Konkrete Anmerkungen & To-Dos

- `User.cs`: Unbenutzte Attribute wie `AdminActions` entfernen  
- `UserService.cs`: 
  - Registrierung besser im `AuthService` bündeln  
  - Fehlerbehandlung bei bereits existierender E-Mail verbessern  
  - Namenskonventionen einheitlich anwenden  
- `AdminService.cs`: Kann gelöscht werden (nicht verwendet)  
- `ReservationSystem.cs`: Umbenennung empfohlen ("ReservationSystem")  
- `FeedbackService.cs`: Nutzer sollten eigenen Kommentar löschen können  
- `ReservationSystemController.cs`:  
  - Logik in `ReservationSystemService` auslagern  
  - Auskommentierten Code entfernen  
- `RestaurantOwnerService.cs`:  
  - Umbenennen in `RestaurantService`  
  - Suchfunktion für Restaurants integrieren  
- `Register`: Nach Registrierung Token zurückgeben  
- **Sicherheit**: Passworthandling prüfen und ggf. optimieren

---

### Zusammenfassung

Das technische Review hat viele wertvolle Einblicke geliefert und zeigt, dass unser Backend in puncto Sicherheit und Qualität auf einem guten Stand ist. Für die nächste Iteration steht klar im Fokus: Code aufräumen, Naming verbessern und einige strukturelle Anpassungen vornehmen.

Wir sind mit dem Ergebnis zufrieden und sehen das Review als wichtigen Schritt zur weiteren Qualitätssteigerung!

**Bis zum nächsten Mal – danke fürs Lesen!**

Liebe Grüße  
**Alina, Alex, Lukas, Moumen und Yahya**
