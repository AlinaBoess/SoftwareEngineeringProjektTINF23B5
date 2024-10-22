```mermaid
---
title: Anmeldevorgang Webseite
---
stateDiagram
    [*] --> EingabeAnmeldedaten: Start
    EingabeAnmeldedaten --> PrüfeDaten: Daten eingegeben
    PrüfeDaten --> Angemeldet: Validierung erfolgreich
    PrüfeDaten --> Fehlermeldung: Validierung fehlgeschlagen
    Fehlermeldung --> EingabeAnmeldedaten: Fehler beheben
    Angemeldet --> [*]: Startseite anzeigen
