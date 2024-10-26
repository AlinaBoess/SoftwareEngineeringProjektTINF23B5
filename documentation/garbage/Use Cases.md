

## Use Case 1: Tisch reservieren

### **Akteure**  
- **Benutzer**: Möchte einen Tisch in einem Restaurant reservieren.  
- **Restaurantreservierungssystem**: Verwaltet die Reservierungen.

### **Vorbedingungen**  
- Der Benutzer ist auf der Website des Restaurantreservierungssystems.  
- Der Benutzer hat ein Restaurant ausgewählt.

### **Nachbedingungen**  
- Die Reservierung ist im System gespeichert und der Benutzer erhält eine Bestätigung.

### **Normaler Ablauf**
1. Der Benutzer wählt ein Restaurant aus der Liste.  
2. Das System zeigt den Raumplan des Restaurants mit verfügbaren Tischen.  
3. Der Benutzer wählt einen freien Tisch aus.  
4. Der Benutzer gibt Datum und Uhrzeit der Reservierung an.  
5. Das System speichert die Reservierung und sendet eine Bestätigung an den Benutzer.

### **Erweiterungen**
- **2a.** Wenn keine Tische verfügbar sind, wird eine Nachricht angezeigt, dass das Restaurant ausgebucht ist.  
- **4a.** Bei unvollständigen oder falschen Eingaben fordert das System den Benutzer zur Korrektur auf.

---

## Use Case 2: Reservierung stornieren

### **Akteure**  
- **Benutzer**: Möchte eine bestehende Reservierung stornieren.  
- **Restaurantreservierungssystem**: Verwaltet die Stornierung.

### **Vorbedingungen**  
- Der Benutzer hat bereits eine Reservierung vorgenommen.

### **Nachbedingungen**  
- Die Reservierung ist im System gelöscht und der Tisch wird wieder als verfügbar angezeigt.

### **Normaler Ablauf**
1. Der Benutzer meldet sich im System an.  
2. Das System zeigt eine Liste der bestehenden Reservierungen.  
3. Der Benutzer wählt die zu stornierende Reservierung aus.  
4. Der Benutzer klickt auf "Stornieren".  
5. Das System löscht die Reservierung und bestätigt die Stornierung.

### **Erweiterungen**
- **3a.** Wenn keine Reservierungen vorhanden sind, wird dem Benutzer eine Nachricht angezeigt.

---

## Use Case 3: Feedback geben

### **Akteure**  
- **Benutzer**: Möchte Feedback zu einer abgeschlossenen Reservierung geben.  
- **Restaurantreservierungssystem**: Verarbeitet das Feedback.

### **Vorbedingungen**  
- Der Benutzer hat eine Reservierung erfolgreich abgeschlossen.

### **Nachbedingungen**  
- Das Feedback ist im System gespeichert und kann von Administratoren eingesehen werden.

### **Normaler Ablauf**
1. Der Benutzer wird nach Abschluss der Reservierung zur Abgabe von Feedback aufgefordert.  
2. Der Benutzer füllt das Feedbackformular aus.  
3. Das System speichert das Feedback und sendet eine Bestätigung an den Benutzer.

### **Erweiterungen**
- **2a.** Bei unvollständigen Eingaben fordert das System den Benutzer zur Vervollständigung des Feedbacks auf.

