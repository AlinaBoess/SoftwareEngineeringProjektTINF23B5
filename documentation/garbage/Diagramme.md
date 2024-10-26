```mermaid

graph TD
  subgraph "Use Case: Tisch reservieren"
    A1[Benutzer] -->|Wählt Restaurant aus| B1[Restaurant anzeigen]
    B1 -->|Zeigt Raumplan| B2[Tisch auswählen]
    B2 -->|Gibt Datum & Uhrzeit an| B3[Reservierung speichern]
    B3 -->|Reservierungsbestätigung senden| A1
  end

  subgraph "Use Case:Reservierung stornieren"

    A2[Benutzer] -->|Meldet sich an| C1[Reservierungen anzeigen]
    C1 -->|Wählt zu stornierende Reservierung| C2[Reservierung stornieren]
    C2 -->|Bestätigt Stornierung| A2
  end

  subgraph "Use Case: Feedback geben"
    A3[Benutzer] -->|Abgeschlossenes Feedback| D1[Feedbackformular anzeigen]
    D1 -->|Füllt Feedback aus| D2[Feedback speichern]
    D2 -->|Sendet Bestätigung| A3
  end
