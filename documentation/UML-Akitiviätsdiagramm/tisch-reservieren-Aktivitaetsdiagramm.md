```mermaid
---
title: Tischreservierung einsehen
---
stateDiagram
    state Entscheidung <<choice>>
    [*] --> BelegtenTischAnklicken: Start
    BelegtenTischAnklicken --> LeseRechtePrüfen: Tisch ausgewählt
    LeseRechtePrüfen --> Entscheidung: Lese-Rechte prüfen
    Entscheidung --> TischInfoAnzeigen: Leseberechtigung vorhanden
    Entscheidung --> InfoAnzeigen: Keine Leseberechtigung
    TischInfoAnzeigen --> [*]: Name und E-Mail anzeigen
    InfoAnzeigen --> [*]: Tisch ist reserviert, keine weiteren Details
