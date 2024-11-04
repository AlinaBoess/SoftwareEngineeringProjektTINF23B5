# Vierter Blogeintrag - Ein Blick in unser Klassendesign

Schön, dass ihr es wieder zu uns geschafft habt! :)
Hier sind ein paar Updates zu unserem Projekt:
Wir haben nun mit der Programmierung begonnen. Hierfür haben wir uns zunächst einige Gedanken über die Struktur unseres Codes gemacht.

## Klassen
Um unser Projekt effizient aufzubauen, haben wir die wichtigsten Klassen modelliert und eine klare Struktur festgelegt.

### Klassendiagramm
Unser Klassendiagramm könnt ihr [hier](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/klassendiagramme.md) einsehen.

### Überlegungen zur Modellierung und angewandte Prinzipien

Bei der Erstellung unseres Klassendiagramms für das Reservations- und Restaurant-Management-System haben wir verschiedene Überlegungen angestellt, um eine übersichtliche und effiziente Struktur zu schaffen. Die wichtigsten Klassen, die verschiedene Benutzerrollen und Komponenten des Systems repräsentieren, ermöglichen eine klare Trennung der Verantwortlichkeiten.

#### Übersicht und Rollenstruktur
Die Benutzerrollen sind durch die Basisklasse `UserBase` realisiert, von der `Admin`, `User` und `RestaurantOwner` erben. So können wir wiederverwendbare Attribute wie `firstName`, `lastName` und `email` zentral verwalten und auch gemeinsame Methoden wie die Passwortüberprüfung einheitlich abwickeln. Die spezifischen Rollen wie `Admin` und `RestaurantOwner` haben eigene Methoden, z. B. das Hinzufügen und Löschen von Restaurants, was den Verwaltungsaufwand erleichtert und die Verantwortlichkeiten klar abgrenzt.

#### Zusammensetzung und Hierarchie in der Restaurantstruktur
Durch die **Komposition** der Klassen `Restaurant`, `Room` und `Table` lässt sich die Struktur eines Restaurants abbilden, das aus mehreren Räumen besteht, die wiederum eine Anzahl von Tischen enthalten. Diese Komposition stellt sicher, dass die Reservierungen auf Tisch-Ebene abgewickelt und verwaltet werden, was für ein Restaurant-Reservierungssystem zentral ist.

#### Einhaltung des Single Responsibility Prinzips (SRP)
Die meisten Klassen folgen dem **Single Responsibility Prinzip (SRP)**, das heißt, sie übernehmen klar abgegrenzte Aufgaben:
   - `ReservationSystem` verwaltet Restaurants und Benutzer.
   - `RestaurantOwner` hat die Verantwortung für Räume und Tische innerhalb des Restaurants.
   - `Reservation` behandelt spezifische Reservierungsinformationen.

So bleibt jede Klasse fokussiert auf eine Hauptaufgabe, was die Wartbarkeit und Testbarkeit des Codes verbessert.

#### Kapselung und Datenzugriff
Wir haben Wert auf **Kapselung** gelegt, indem wir Attribute als privat deklariert haben und den Zugriff über Getter- und Setter-Methoden regeln. Zum Beispiel sind Attribute wie `passwordHash` und die Listen für Benutzer und Restaurants im `ReservationSystem` nur innerhalb der Klasse direkt zugänglich. So können sensible Daten nicht außerhalb der Klasse manipuliert werden und der Datenzugriff bleibt kontrolliert.

#### Anwendung von Polymorphie
Durch die Vererbung von `UserBase` können wir polymorphe Methoden wie `AddUser()` und `RemoveUser()` verwenden, um alle Benutzerrollen flexibel zu verwalten. Dies erleichtert den Code erheblich, da die Methoden ohne Anpassung für verschiedene Benutzerarten funktionieren.

#### Beziehungen und Interaktionen zwischen den Hauptkomponenten
Das `ReservationSystem` fungiert als zentrale Verwaltungseinheit und ist für die Verwaltung von Restaurants und Benutzern verantwortlich. Diese Struktur ermöglicht eine saubere Trennung der Logik, ohne die Klassen zu stark zu koppeln. Reservierungen werden über die `Reservation`-Klasse verwaltet, die alle notwendigen Daten wie den Ersteller (User), Tisch und Zeiträume enthält.

#### Weitere Design-Entscheidungen
   - `Admin` hat spezielle Methoden wie `createRestaurant()` und `deleteRestaurant()` und kann so die Restaurantstruktur steuern.
   - `Table` hat eine statische Methode `Reserve()`, was es erleichtert, eine Reservierung effizient zu erstellen.

### Zusammenfassung der angewandten Prinzipien

Unsere wichtigsten Prinzipien bei der Modellierung waren:
- **Single Responsibility Principle (SRP)** für klare Aufgabenverteilung.
- **Encapsulation** zur Sicherung und Kontrolle des Datenzugriffs.
- **Inheritance und Polymorphie** zur flexiblen Benutzerverwaltung.
- **Komposition** zur Abbildung der Restaurant-Hierarchie.

Diese Struktur unterstützt die klare Aufgabentrennung und einfache Wartbarkeit des Systems.

## Sonstige Aufgaben
Ansonsten haben wir uns noch mit Folgendem beschäftigt:
- Weiterführende Strukturierung unseres GitHub Repositorys zur besseren Übersicht
- Implementierung der Klassenattribute sowie die ersten Methoden in den zentralen Klassen.

Insgesamt sind wir mit dem Fortschritt zufrieden und freuen uns, bald die ersten Funktionen zu zeigen.
Schaut gerne beim nächsten Mal wieder vorbei, wir freuen uns auf euch ;)
Bei Wünschen oder Anregungen dürft ihr uns gerne schreiben - wir freuen uns auf euer Feedback!

Liebe Grüße 
Lukas, Alex, Moumen, Yahya und Alina
