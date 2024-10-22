```mermaid
---
title: Anmeldevorgang Webseite
---
stateDiagram
    state if_state <<choice>>
    [*] --> EingabeAnmeldedaten: Start
    EingabeAnmeldedaten --> PrüfeDaten: Daten eingegeben
    PrüfeDaten --> if_state
    if_state --> Angemeldet: Validierung erfolgreich
    if_state --> Fehlermeldung: Validierung fehlgeschlagen
    Fehlermeldung --> EingabeAnmeldedaten: Fehler beheben
    Angemeldet --> [*]: Startseite anzeigen
