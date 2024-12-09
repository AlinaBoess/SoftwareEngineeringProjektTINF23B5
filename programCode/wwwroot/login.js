document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault();
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const loginData = {
        Email: email,
        Password: password
    };

    try {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                 loginData
            },
            body: JSON.stringify(loginData)
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('token', data.token);
            window.location.href = '/index.html';
            console.log("bearer " + data.token);
        }
        else {
            console.error("Anmeldung fehlgeschlagen", response.statusText);
        }
    } catch (error) {
        console.error("Fehler bei der Anmeldung", error)
    }
});

