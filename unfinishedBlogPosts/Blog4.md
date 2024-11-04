# Vierter Blogeintrag - Ein Blick in unser Klassendesign

Schön, dass ihr wieder bei uns seid! :) In der letzten Woche haben wir intensiv an der Struktur unseres Projekts gearbeitet und sind stolz darauf, die Programmierung gestartet zu haben. In diesem Blogeintrag geben wir euch einen Einblick in die wesentlichen Klassen und Prinzipien, die unser Reservations- und Restaurant-Management-System prägen.

## Klassen
Um unser Projekt effizient zu gestalten, haben wir die zentralen Klassen modelliert und eine klare Struktur definiert.

### Klassendiagramm
Unser Klassendiagramm könnt ihr [hier](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/klassendiagramme.md) einsehen.

### Überlegungen zur Modellierung und angewandte Prinzipien

Bei der Erstellung unseres Klassendiagramms für das Reservations- und Restaurant-Management-System haben wir verschiedene Überlegungen angestellt, um eine übersichtliche und effiziente Struktur zu schaffen. 

Die wichtigsten Klassen, die verschiedene Benutzerrollen und Komponenten des Systems repräsentieren, ermöglichen eine klare Trennung der Verantwortlichkeiten.

#### Übersicht und Rollenstruktur
Die Benutzerrollen sind durch die Basisklasse `UserBase` realisiert, von der `Admin`, `User` und `RestaurantOwner` erben. So können wir wiederverwendbare Attribute wie `firstName`, `lastName` und `email` zentral verwalten und auch gemeinsame Methoden wie die Passwortüberprüfung einheitlich abwickeln. 

Die spezifischen Rollen wie `Admin` und `RestaurantOwner` haben eigene Methoden, z. B. das Hinzufügen und Löschen von Restaurants, was den Verwaltungsaufwand erleichtert und die Verantwortlichkeiten klar abgrenzt.

#### Zusammensetzung und Hierarchie in der Restaurantstruktur
Durch die **Komposition** der Klassen `Restaurant`, `Room` und `Table` lässt sich die Struktur eines Restaurants abbilden, das aus mehreren Räumen besteht, die wiederum eine Anzahl von Tischen enthalten. 

Diese Komposition stellt sicher, dass die Reservierungen auf Tisch-Ebene abgewickelt und verwaltet werden, was für ein Restaurant-Reservierungssystem zentral ist.

### Einhaltung der SOLID-Prinzipien in unserem Design

Unser Design berücksichtigt verschiedene Prinzipien der **SOLID**-Architektur, wobei wir uns bewusst auf das wichtigste Prinzip für unsere Anwendungsfälle konzentriert haben.

#### Single Responsibility Principle (SRP) – *Konsequent erfüllt*
Das **Single Responsibility Principle** besagt, dass jede Klasse nur eine Verantwortung haben sollte. 

Unser Design stellt dieses Prinzip sicher, indem jede Klasse nur eine Aufgabe und einen klar definierten Zweck hat:
   - `ReservationSystem` verwaltet Restaurants und Benutzer.
   - `RestaurantOwner` ist verantwortlich für die Verwaltung von Räumen und Tischen in einem Restaurant.
   - `Reservation` behandelt spezifische Reservierungsinformationen.

Durch diese klare Aufgabenverteilung ist der Code wartbar und die einzelnen Module sind gut isoliert. Jede Klasse kann einfach getestet und bei Bedarf erweitert werden, ohne die Verantwortlichkeiten der anderen Klassen zu beeinträchtigen.

> **Begründung**: Das SRP bildet das Fundament für sauberen, modularen Code und ist entscheidend für die Wartbarkeit und Erweiterbarkeit. Daher haben wir uns darauf konzentriert, SRP konsequent umzusetzen.

#### Open-Closed Principle (OCP) – *Teilweise erfüllt*
Das **Open-Closed Principle** fordert, dass Klassen offen für Erweiterungen, aber geschlossen für Modifikationen sein sollen. 

Wir haben das Prinzip durch Vererbung berücksichtigt, insbesondere bei der Verwaltung der Benutzerrollen `Admin`, `User`, und `RestaurantOwner`, die alle von `UserBase` erben. So können wir neue Benutzerrollen hinzufügen, ohne bestehende Klassen direkt ändern zu müssen.

> **Begründung**: Wir haben uns entschieden, OCP teilweise zu berücksichtigen, um unseren Code flexibel für Erweiterungen zu gestalten. Da unser Projekt jedoch überschaubar ist und die Benutzerrollen stabil bleiben, halten wir eine volle Umsetzung durch Schnittstellen in dieser Phase nicht für notwendig.

#### Liskov Substitution Principle (LSP) – *Teilweise erfüllt*
Das **Liskov Substitution Principle** besagt, dass Objekte von Unterklassen in allen Kontexten wie ihre Oberklassen funktionieren sollten.

Unser Projekt nutzt polymorphe Benutzerrollen, die alle `UserBase` als Oberklasse verwenden.

Somit kann jeder Benutzer (egal ob `User`, `Admin` oder `RestaurantOwner`) in Kontexten eingesetzt werden, in denen ein allgemeiner Benutzer erwartet wird, ohne die Funktionsweise zu ändern.

> **Begründung**: Unser Klassendesign ist so strukturiert, dass die Benutzerrollen die `UserBase`-Schnittstellen problemlos nutzen können. Da dies jedoch kein besonders komplexer Anwendungsfall ist, halten wir eine tiefere Umsetzung des LSP für weniger relevant.

#### Interface Segregation Principle (ISP) – *Ansatzweise umgesetzt*
Das **Interface Segregation Principle** besagt, dass Klassen nicht gezwungen sein sollten, Methoden zu implementieren, die sie nicht benötigen. 

Aktuell berücksichtigen wir dieses Prinzip teilweise, indem wir den Rollen spezifische Methoden zugeordnet haben.

So enthält `Admin` Methoden zur Restaurantverwaltung, während `User` eher auf Reservationen fokussiert ist.

> **Begründung**: Für unser Projekt haben wir uns entschieden, auf spezifische Interfaces zu verzichten, um die Komplexität nicht unnötig zu steigern. Für einfache Benutzerrollen in einem kleinen Projekt sehen wir die Nutzung von Schnittstellen als optional an und fokussieren uns stattdessen auf klare, durch Vererbung getrennte Rollen.

#### Dependency Inversion Principle (DIP) – *Noch nicht umgesetzt*
Das **Dependency Inversion Principle** fordert, dass High-Level-Module nicht direkt von Low-Level-Modulen abhängig sein sollten, sondern beide von Abstraktionen abhängen.

Momentan sind die Abhängigkeiten in unserem Code noch konkret, d.h., das `ReservationSystem` verwaltet direkt Instanzen von `Restaurant` und `UserBase`. 

> **Begründung**: Aufgrund der begrenzten Größe unseres Projekts haben wir uns entschieden, das DIP-Prinzip in dieser Phase nicht konsequent umzusetzen. Unsere Komponenten sind bereits lose gekoppelt, und zusätzliche Abstraktionen wären im aktuellen Umfang unnötig komplex.

### Zusammenfassung der angewandten Prinzipien

Unsere wichtigsten Prinzipien bei der Modellierung waren:

- **Single Responsibility Principle (SRP)** für klare Aufgabenverteilung und übersichtliche, gut testbare Klassen.
- **Open-Closed Principle (OCP)**, um durch Vererbung für Erweiterungen offen zu bleiben, speziell bei den Benutzerrollen.
- **Liskov Substitution Principle (LSP)**, um sicherzustellen, dass unterschiedliche Benutzerrollen einheitlich genutzt werden können.

Diese Struktur unterstützt eine klare Aufgabentrennung und einfache Erweiterbarkeit, ohne die Komplexität unnötig zu erhöhen.

## Sonstige Aufgaben
Ansonsten haben wir uns noch mit Folgendem beschäftigt:
- Weiterführende Strukturierung unseres GitHub Repositorys zur besseren Übersicht
- Implementierung der Klassenattribute sowie die ersten Methoden in den zentralen Klassen.

Insgesamt sind wir mit dem Fortschritt zufrieden und freuen uns, bald die ersten Funktionen zu zeigen.

Unsere aktualisierten Sequenzdiagramme vom letzten Mal könnt ihr euch auch gerne [hier](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/3) noch einmal anschauen.

Schaut gerne beim nächsten Mal wieder vorbei, wir freuen uns auf euch ;) 

Bei Wünschen oder Anregungen dürft ihr uns gerne schreiben - wir freuen uns auf euer Feedback!

Liebe Grüße 

Lukas, Alex, Moumen, Yahya und Alina
