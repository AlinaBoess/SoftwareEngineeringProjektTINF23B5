Restaurant-Buchungssystem

#### Use-Case-Realization Specification: Reservierung vornehmen
**Version:** 1.0  
**Issue Date:** 27.10.2024  
**Confidential**  

---

### Revision History
| Date        | Version | Description                | Author |
|-------------|---------|----------------------------|--------|
| `27/10/2024` | 1.0     | Initial document creation | `Restaurant Reservierung`|

---
### UCRS1:

### 1. Introduction

#### 1.1 Purpose
Der Zweck dieser Use-Case-Realization Specification ist es, den Ablauf und die Interaktionen zu dokumentieren, die erforderlich sind, um den Use-Case „Reservierung vornehmen“ im Restaurant-Buchungssystem zu realisieren.

#### 1.2 Scope
Dieses Dokument beschreibt die Schritte, die ein Benutzer (Kunde) durchführt, um eine Tischreservierung vorzunehmen, sowie die damit verbundenen Interaktionen zwischen Frontend und Backend des Systems.

#### 1.3 Definitions, Acronyms, and Abbreviations
- **Benutzer**: Kunde, der eine Reservierung vornimmt.
- **Frontend**: Benutzeroberfläche des Systems.
- **Backend**: Server-seitige Verarbeitung und Datenverwaltung.
- **Reservierung**: Eine vom Benutzer getätigte Buchung für einen bestimmten Tisch zu einer bestimmten Zeit.

#### 1.4 References
![Reservierung vornehmen](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/082cfdb95cc8353bb9843dec2bdcf50d49c8e8dd/documentation/SRS/Bilder/Reservieren.png)


#### 1.5 Overview
Das Dokument beschreibt die Schritte und Datenflüsse, die notwendig sind, um eine Tischreservierung im System zu erstellen, einschließlich Validierung und Bestätigung.

---

### 2. Flow of Events—Design

**Diagram: Reservierung vornehmen Sequence**

1. **Benutzereingabe**: Der Benutzer gibt das gewünschte Datum, die Uhrzeit und die Anzahl der Personen im Frontend an, um nach verfügbaren Tischen zu suchen.
2. **Frontend-Überprüfung**: Das Frontend überprüft, ob alle erforderlichen Informationen eingegeben wurden, und sendet die Anfrage an das Backend.
3. **Backend-Validierung**: Das Backend sucht nach freien Tischen, die die Anforderungen des Benutzers erfüllen (Kapazität, Verfügbarkeit).
4. **Verfügbarkeitsanzeige**: Das Backend sendet die Liste der verfügbaren Tische zurück an das Frontend, das diese dem Benutzer anzeigt.
5. **Auswahl und Bestätigung**: Der Benutzer wählt einen Tisch aus und bestätigt die Reservierung.
6. **Backend-Bestätigung**: Das Backend erstellt die Reservierung und speichert sie in der Datenbank. Eine Bestätigungsnachricht wird an das Frontend zurückgesendet.
7. **Frontend-Rückmeldung**: Das Frontend zeigt dem Benutzer eine Bestätigung der Reservierung an, einschließlich der Details zur Reservierung.

---

### 3. Derived Requirements

- **Datensicherheit**: Sicherstellen, dass die Übertragung der Reservierungsdaten über HTTPS erfolgt.
- **Benutzerfreundliches Feedback**: Dem Benutzer eine klare Rückmeldung über den Status der Reservierung geben.
- **Verfügbarkeit**: Das System muss in Echtzeit die Verfügbarkeit der Tische überprüfen können, um Doppelbuchungen zu vermeiden.

---

### UCRS2:


### Use-Case-Realization Specification: Registrierung

### 1. Introduction

#### 1.1 Purpose
Der Zweck dieser Use-Case-Realization Specification ist es, die erforderlichen Schritte und Interaktionen zu dokumentieren, die für die Realisierung des Registrierung-Use-Cases im Restaurant-Buchungssystem notwendig sind.

#### 1.2 Scope
Dieses Dokument beschreibt den Ablauf der Registrierung eines neuen Nutzers und die Interaktionen zwischen dem Benutzer, dem Frontend und dem Backend, die zur Erstellung eines neuen Benutzerkontos führen.

#### 1.3 Definitions, Acronyms, and Abbreviations
- **Benutzer**: Eine Person, die sich im System registriert, um ein Konto zu erstellen.
- **Frontend**: Die Benutzeroberfläche, über die der Benutzer interagiert.
- **Backend**: Server-seitige Komponenten, die die Registrierung verarbeiten und Daten speichern.
- **Bestätigung**: Die positive Rückmeldung nach erfolgreicher Registrierung.
- **Fehlermeldung**: Die Rückmeldung bei Fehlern, z. B. wenn die E-Mail bereits registriert ist.

#### 1.4 References
![Register](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/082cfdb95cc8353bb9843dec2bdcf50d49c8e8dd/documentation/SRS/Bilder/Register.png)

#### 1.5 Overview
Das Dokument beschreibt den Ablauf der Registrierung und die zugehörigen Interaktionen zwischen Benutzer, Frontend und Backend. Es zeigt die Überprüfung der Eingaben, die Verarbeitung im Backend und die Rückmeldung an den Benutzer.

---

### 2. Flow of Events—Design

**Diagram: Register Sequence**

1. **Benutzereingabe**: Der Benutzer gibt seine E-Mail-Adresse und ein Passwort in das Registrierungsformular im Frontend ein.
2. **Datensenden**: Das Frontend überprüft die Eingaben auf Vollständigkeit und sendet die E-Mail-Adresse und das Passwort an das Backend zur Verarbeitung.
3. **Backend-Verarbeitung**: Das Backend erhält die Registrierungsdaten und führt folgende Schritte aus:
   - **Überprüfung der E-Mail-Adresse**: Das Backend prüft, ob die E-Mail-Adresse bereits in der Datenbank existiert.
   - **Kontoerstellung**: Falls die E-Mail-Adresse noch nicht vorhanden ist, erstellt das Backend ein neues Benutzerkonto mit den übermittelten Daten und speichert es in der Datenbank.
4. **Rückmeldung**: Das Backend sendet eine Rückmeldung an das Frontend:
   - **Erfolg**: Bei erfolgreicher Registrierung wird eine Bestätigung zurückgesendet.
   - **Fehler**: Wenn die E-Mail-Adresse bereits existiert oder ein anderer Fehler auftritt, wird eine entsprechende Fehlermeldung gesendet.
5. **Anzeige für den Benutzer**: Das Frontend zeigt dem Benutzer die Rückmeldung an:
   - **Bestätigung**: Bei Erfolg wird eine Registrierungsbestätigung angezeigt.
   - **Fehlermeldung**: Bei Fehlern wird dem Benutzer die entsprechende Fehlermeldung angezeigt und er wird gebeten, die Eingaben zu korrigieren.

---

### 3. Derived Requirements

- **Eindeutigkeit der E-Mail-Adresse**: Sicherstellen, dass jede E-Mail-Adresse nur einmal in der Datenbank registriert werden kann, um doppelte Benutzerkonten zu vermeiden.
- **Sicherheit der Passwörter**: Die Passwörter sollten verschlüsselt in der Datenbank gespeichert werden, um die Sicherheit der Nutzerdaten zu gewährleisten.
- **Benutzerfreundliche Fehlermeldungen**: Detaillierte und hilfreiche Fehlermeldungen sollten angezeigt werden, wenn die Registrierung fehlschlägt (z. B. bereits registrierte E-Mail).
- **Echtzeit-Feedback**: Die Eingaben sollten auf der Benutzeroberfläche validiert werden, um eine bessere Benutzererfahrung zu gewährleisten.
