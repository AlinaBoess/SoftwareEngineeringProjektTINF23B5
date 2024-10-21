

# Restaurantreservierungssystem

**Software Requirements Specification**  
Version 1.0

---

## 1. Einführung

### 1.1 Zweck
Dieses SRS-Dokument beschreibt alle Anforderungen für die Restaurantreservierungs-Webanwendung. Es enthält sowohl funktionale als auch nicht-funktionale Anforderungen und dient als Grundlage für die Entwicklung, das Testen und künftige Erweiterungen.

### 1.2 Umfang
Die Anwendung ermöglicht es Benutzern, verfügbare Restaurants zu durchsuchen, Raumpläne zu sehen, Tische zu reservieren, Reservierungen zu stornieren und Feedback zu geben. Das System verwaltet Reservierungen, Benutzeranmeldungen und die Sammlung von Rückmeldungen und unterstützt sowohl Desktop- als auch mobile Browser.

### 1.3 Definitionen, Akronyme und Abkürzungen
- **SRS**: Software Requirements Specification (Software-Anforderungsdokument)  
- **IDE**: Integrated Development Environment (Integrierte Entwicklungsumgebung)  
- **C#**: Programmiersprache für das Backend  
- **HTML/CSS/JS**: Technologien für das Frontend  
- **ASP.NET Core**: Framework zur Entwicklung von Webanwendungen im Backend  

### 1.4 Referenzen
- Projekt-Blog: [Restaurantreservierungstool Blog]
- Jira Board für Scrum und Backlog Management

### 1.5 Überblick
Dieses Dokument ist in Abschnitte gegliedert, die die Produktvision, Anwendungsfälle sowie detaillierte funktionale und nicht-funktionale Anforderungen beschreiben. Nachfolgende Abschnitte liefern detaillierte Informationen zu diesen Anforderungen und geben Einblicke in die Architektur und Einschränkungen des Projekts.

---

## 2. Gesamtbeschreibung

### 2.1 Vision
Das Ziel dieses Projekts ist es, eine benutzerfreundliche Webanwendung zu entwickeln, die es Benutzern ermöglicht, Tische in verschiedenen Restaurants zu reservieren. Die Anwendung zeigt Raumpläne an, in denen Benutzer freie Tische auswählen und reservieren können. Darüber hinaus können Benutzer Reservierungen stornieren und nachträglich Feedback geben.

### 2.2 Anwendungsfalldiagramm
Wird noch festgelegt. (Hier wird die Interaktion zwischen den Benutzern und dem System grafisch dargestellt.)

### 2.3 Technologiestack
- **Backend**: C# mit ASP.NET Core  
- **Frontend**: HTML, CSS, JavaScript/TypeScript  
- **Datenbank**: Noch zu entscheiden (voraussichtlich SQL-basiert)  
- **IDE**: Visual Studio  
- **Projektmanagement**: Jira  
- **Versionskontrolle**: Git (GitHub)

---

## 3. Spezifische Anforderungen

### 3.1 Funktionalität

#### 3.1.1 Auswahl von Restaurants und Tischreservierung
- Benutzer können verfügbare Restaurants durchsuchen.
- Raumpläne zeigen verfügbare Tische an.
- Benutzer können freie Tische auswählen und reservieren.
- Nach erfolgreicher Reservierung wird eine Bestätigung an den Benutzer gesendet.

#### 3.1.2 Stornierung von Reservierungen
- Benutzer können ihre Reservierungen stornieren.
- Bei einer Stornierung erhält der Benutzer eine Benachrichtigung zur Bestätigung.

#### 3.1.3 Feedbacksystem
- Nach einer Reservierung werden Benutzer aufgefordert, Feedback zu geben.

### 3.2 Benutzbarkeit
- Das System wird intuitiv gestaltet, sodass Benutzer keine lange Einarbeitungszeit benötigen.
- Die Benutzeroberfläche orientiert sich an gängigen Webdesign-Konventionen, um eine positive Benutzererfahrung zu gewährleisten.

### 3.3 Zuverlässigkeit
- **Verfügbarkeit**: Das System sollte zu 95 % der Zeit verfügbar sein.
- **MTTR** (Mean Time to Repair): Die mittlere Reparaturzeit nach einem Ausfall sollte unter 4 Stunden liegen.
- **Fehlerrate**: Ziel ist es, die Fehlerrate bei < 0,1 Fehlern pro 1000 Codezeilen zu halten.

### 3.4 Leistung
- **Reaktionszeit**: Jede Interaktion (z.B. Tischauswahl) sollte innerhalb von 2 Sekunden abgeschlossen sein.
- **Kapazität**: Das System sollte tausende gleichzeitige Benutzer unterstützen können.

### 3.5 Wartbarkeit
- Der Code wird nach den Prinzipien des "Clean Code" geschrieben, um die Wartbarkeit zu erleichtern.
- Eine umfassende Dokumentation wird während des gesamten Projekts erstellt und aktualisiert.

### 3.6 Entwurfseinschränkungen
- **Programmiersprache**: Das Backend wird mit C# und ASP.NET Core entwickelt.
- **Datenbank**: Die Wahl der Datenbank erfolgt nach Kompatibilitätsanforderungen mit ASP.NET Core.

### 3.7 Anforderungen an die Online-Benutzerdokumentation und das Hilfesystem
- Das System wird eine Hilfeseite mit häufig gestellten Fragen (FAQ) und Kontaktinformationen zum technischen Support bereitstellen.

### 3.8 Gekaufte Komponenten
Aktuell wurden noch keine externen Komponenten gekauft.

### 3.9 Schnittstellen

#### 3.9.1 Benutzerschnittstellen
- Dashboard, das die Verfügbarkeit von Restaurants und Tischen anzeigt.
- Buchungsbestätigungs- und Reservierungsverwaltungsschnittstelle.
- Feedbackformular.

#### 3.9.2 Softwareschnittstellen
- Das Backend kommuniziert mit dem Frontend über REST-APIs.

---

## 4. Unterstützende Informationen
Weitere Anhänge oder Diagramme werden hier hinzugefügt, um das Verständnis zu erleichtern.

---

Damit hast du eine übersichtliche und gut strukturierte Fassung des SRS-Dokuments im Markdown-Format. Wenn du Anpassungen oder Ergänzungen brauchst, stehe ich dir zur Verfügung!
