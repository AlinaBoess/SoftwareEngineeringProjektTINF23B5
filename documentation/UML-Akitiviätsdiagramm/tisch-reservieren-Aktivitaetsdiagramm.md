```mermaid
---
title: Reservierung einsehen
---
stateDiagram
    state if_state <<choice>>
    [*] --> TischAnklicken: start
    TischAnklicken --> LeseRechtePrüfen
    LeseRechtePrüfen --> if_state
    if_state --> TischInfoAnzeigen: Nutzer hat Leseberechtigung
    if_state --> InfoAnzeigen: Nutzer hat keine Leseberechtigung
    TischInfoAnzeigen --> [*]: Name, Email anzeigen
    InfoAnzeigen --> [*]: Tisch ist reserviert
