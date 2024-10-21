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
Der Zweck dieser Software Requirements Specification (SRS) ist es, eine klare und umfassende Beschreibung der Anforderungen für das Restaurant-Buchungssystem bereitzustellen. Sie beschreibt das externe Verhalten des Systems und gibt sowohl funktionale als auch nicht-funktionale Anforderungen vor. Die SRS dient als zentrale Referenz für alle Beteiligten und stellt sicher, dass die Anforderungen an die Software klar und nachvollziehbar dokumentiert sind.

### 1.2 Umfang  
Das Restaurant-Buchungssystem wird als Webanwendung entwickelt. Diese Anwendung ermöglicht Benutzern (Kunden), Reservierungen vorzunehmen, und Administratoren (Restaurantmitarbeitern), Tische und Reservierungen zu verwalten.
- Benutzer (Kunden): Können verfügbare Tische einsehen, Reservierungen vornehmen, stornieren und Feedback hinterlassen.
- Administratoren: Verwalten die Tische, sehen Reservierungen ein und bearbeiten sie.
Das System wird mit C#, ASP.NET, HTML, JavaScript/TypeScript und CSS umgesetzt und verwendet eine SQL-Datenbank zur Speicherung der Daten.

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
Dieses Dokument ist eine Software Requirements Specification (SRS) für das Restaurant Buchungssystem. Es beschreibt die Anforderungen, die zur Entwicklung der Anwendung erforderlich sind, sowie deren Funktionalität und nicht-funktionale Eigenschaften.
Die SRS ist in die folgenden Hauptabschnitte gegliedert:
1. **Einführung**: Dieser Abschnitt bietet einen Überblick über die Ziele und den Umfang des Dokuments sowie wichtige Definitionen und Referenzen.
2. **Gesamtbeschreibung**: Hier werden die allgemeine Funktionsweise des Systems, die Hauptakteure und deren Interaktionen mit dem System erläutert. Auch werden die wichtigsten Features und Teilsysteme des Restaurant Buchungssystems vorgestellt.
3. **Spezifische Anforderungen**: In diesem Abschnitt werden die detaillierten funktionalen und nicht-funktionalen Anforderungen an das System aufgeführt. Dazu gehören unter anderem Anforderungen an die Benutzeroberfläche, Datenverwaltung, Sicherheitsanforderungen und Performance.
4. **Unterstützende Informationen**: Hier werden zusätzliche Informationen bereitgestellt, die für das Verständnis der SRS relevant sein könnten, einschließlich Glossar, Abkürzungen und andere nützliche Hinweise.
Diese Struktur soll sicherstellen, dass alle Aspekte des Restaurant Buchungssystems klar und umfassend dokumentiert sind, um eine effektive Entwicklung und Implementierung der Software zu ermöglichen.

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
