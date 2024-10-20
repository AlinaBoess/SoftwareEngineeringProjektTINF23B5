# Restaurant Buchungssystem - Software Requirements Specification

## Inhaltsverzeichnis
1. [Einführung](#1-einführung)  
   1.1 [Zweck](#11-zweck)  
   1.2 [Umfang](#12-umfang)  
   1.3 [Definitionen, Akronyme und Abkürzungen](#13-definitionen-akronyme-und-abkürzungen)  
   1.4 [Referenzen](#14-referenzen)  
   1.5 [Überblick](#15-überblick)  
2. [Gesamtbeschreibung](#2-gesamtbeschreibung)  
3. [Spezifische Anforderungen](#3-spezifische-anforderungen)  
   3.1 [Funktionalität](#31-funktionalität)  
   3.2 [Benutzbarkeit](#32-benutzbarkeit)  
   3.3 [Zuverlässigkeit](#33-zuverlässigkeit)  
   3.4 [Leistung](#34-leistung)  
   3.5 [Unterstützbarkeit](#35-unterstützbarkeit)  
   3.6 [Design-Beschränkungen](#36-design-beschränkungen)  
   3.7 [Online-Benutzerdokumentation und Hilfesystemanforderungen](#37-online-benutzerdokumentation-und-hilfesystemanforderungen)  
   3.8 [Gekaufte Komponenten](#38-gekaufte-komponenten)  
   3.9 [Schnittstellen](#39-schnittstellen)  
   3.10 [Lizenzierungsanforderungen](#310-lizenzierungsanforderungen)  
   3.11 [Rechtliche, urheberrechtliche und andere Hinweise](#311-rechtliche-urheberrechtliche-und-andere-hinweise)  
   3.12 [Anwendbare Standards](#312-anwendbare-standards)  
4. [Unterstützende Informationen](#4-unterstützende-informationen)  

## 1. Einführung  

### 1.1 Zweck  
Der Zweck dieser Software Requirements Specification (SRS) ist es, eine klare und umfassende Beschreibung der Anforderungen für das Restaurant-Buchungssystem bereitzustellen. Diese Spezifikation soll die folgenden Ziele erreichen:
- Beschreibung des Systems: Das SRS beschreibt das externe Verhalten des Buchungssystems, einschließlich der Benutzerinteraktionen und der zu erwartenden Systemreaktionen. Es soll den Stakeholdern helfen, ein klares Verständnis darüber zu bekommen, wie das System funktionieren wird.
- Funktionale Anforderungen: Die SRS enthält detaillierte funktionale Anforderungen, die die spezifischen Funktionen und Dienstleistungen beschreiben, die das System bereitstellen muss, um die Bedürfnisse der Benutzer und des Restaurantbetriebs zu erfüllen. Dazu gehören unter anderem die Erstellung, Änderung und Stornierung von Reservierungen sowie die Verwaltung von Tischen und Zeiten.
- Nicht-funktionale Anforderungen: Neben den funktionalen Anforderungen werden auch nicht-funktionale Anforderungen behandelt. Diese umfassen Leistungsanforderungen (z. B. Antwortzeiten), Zuverlässigkeit, Sicherheit, Benutzbarkeit und andere Qualitätsmerkmale, die das Benutzererlebnis und die Systemleistung beeinflussen.
- Designbeschränkungen: Die SRS identifiziert Designbeschränkungen und Einschränkungen, die sich aus der technologischen Umgebung, den gesetzlichen Vorgaben oder den betrieblichen Gegebenheiten des Restaurants ergeben können.
- Komplette und umfassende Beschreibung: Diese Spezifikation zielt darauf ab, alle Faktoren zu berücksichtigen, die für eine vollständige und umfassende Beschreibung der Anforderungen an die Software erforderlich sind. Sie dient als Grundlage für die Entwicklung, das Testen und die Implementierung des Systems sowie für die Schulung der Benutzer.
Diese SRS soll somit als Referenzdokument für alle Beteiligten dienen und sicherstellen, dass alle Anforderungen an das Restaurant-Buchungssystem klar und verständlich festgehalten sind.

### 1.2 Umfang  
Das Projekt wird als Webanwendung entwickelt, die eine Interaktion zwischen Benutzern (Kunden) und Restaurantmitarbeitern (Administratoren) ermöglicht.
- Akteure
   - Benutzer: Kunden, die Reservierungen vornehmen.
   - Administratoren: Restaurantmitarbeiter, die Tische verwalten und Reservierungen einsehen.

- Geplante Teilsysteme
   - Tischverwaltung: Administratoren können Tische erstellen und die verfügbaren Reservierungen einsehen.
   - Tischplan und Verfügbarkeitsanzeige: Benutzer können einen interaktiven Tischplan einsehen und einen gewünschten Zeitblock wählen.
   - Reservierungssystem: Benutzer können Tische reservieren, erhalten eine Bestätigung und können Reservierungen stornieren.
   - Feedbacksystem: Benutzer haben die Möglichkeit, Feedback zu ihren Erfahrungen abzugeben.
   - Benutzersystem: Erstellung von Konten für Benutzer und Administratoren.
- Technische Umsetzung: Die Webanwendung wird in C#, ASP.NET, HTML, JavaScript/TypeScript und CSS entwickelt und nutzt eine SQL-Datenbank.

### 1.3 Definitionen, Akronyme und Abkürzungen  
- **SRS (Software Requirements Specification)**: Ein Dokument, das die Anforderungen an eine Softwareanwendung beschreibt.
- **Benutzer**: Eine Person, die die Webanwendung verwendet, um Reservierungen vorzunehmen und Informationen über verfügbare Tische abzurufen.
- **Administrator**: Ein Mitarbeiter des Restaurants, der das System verwaltet, Tische erstellt und Reservierungen einsehen kann.
- **Tischplan**: Eine grafische Darstellung der verfügbaren Tische in einem Restaurant, die Benutzern zeigt, welche Tische frei oder reserviert sind.
- **Reservierung**: Der Vorgang, bei dem ein Benutzer einen Tisch für einen bestimmten Zeitpunkt reserviert, um sicherzustellen, dass der Tisch zur gewünschten Zeit verfügbar ist.
- **SQL (Structured Query Language)**: Eine standardisierte Programmiersprache zur Verwaltung und Abfrage von Daten in relationalen Datenbanken.
- **ASP.NET**: Ein Framework von Microsoft zur Entwicklung von Webanwendungen und Webdiensten.
- **HTML (Hypertext Markup Language)**: Eine Markup-Sprache zur Strukturierung und Darstellung von Inhalten im Web.
- **CSS (Cascading Style Sheets)**: Eine Stylesheet-Sprache zur Gestaltung von HTML-Inhalten, die das Aussehen und Layout von Webseiten definiert.
- **JavaScript/TypeScript**: Programmiersprachen, die verwendet werden, um interaktive und dynamische Inhalte auf Webseiten zu erstellen.

### 1.4 Referenzen  
Die folgende Liste enthält alle Dokumente und Quellen, auf die im Rahmen dieser Software Requirements Specification (SRS) verwiesen wird:
1. **Template von RUP SRS**  
   Titel: RUP Software Requirements Specification (SRS) Template  
   Link: [RUP SRS Template](https://sceweb.sce.uhcl.edu/helm/REQ_ENG_WEB/My-Files/mod4/rup_srs.dot)
2. **Projektdokumentation**  
   Titel: Restaurant Buchungssystem – Projektdokumentation  
   Autoren: Alina Moumen, Yahya, Alex, Lukas  
   Link: [Dokumentation auf GitHub](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/tree/main/documentation)  
3. **Code-Repository**  
   Titel: Restaurant Buchungssystem – Code-Repository  
   Autoren: Alina Moumen, Yahya, Alex, Lukas  
   Link: [Code auf GitHub](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/tree/main/programCode)  
4. **Blog**  
   Titel: Restaurant Buchungssystem – Wöchentliche Blog-Beiträge  
   Autoren: Alina Moumen, Yahya, Alex, Lukas  
   Link: [Blog auf GitHub Discussions](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions)  

### 1.5 Überblick  

## 2. Gesamtbeschreibung  

## 3. Spezifische Anforderungen  
### 3.1 Funktionalität  
### 3.2 Benutzbarkeit  
### 3.3 Zuverlässigkeit  
### 3.4 Leistung  
### 3.5 Unterstützbarkeit  
### 3.6 Design-Beschränkungen  
### 3.7 Online-Benutzerdokumentation und Hilfesystemanforderungen  
### 3.8 Gekaufte Komponenten  
### 3.9 Schnittstellen  
### 3.10 Lizenzierungsanforderungen  
### 3.11 Rechtliche, urheberrechtliche und andere Hinweise  
### 3.12 Anwendbare Standards  

## 4. Unterstützende Informationen  
