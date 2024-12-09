document.getElementById("fetchDataButton").addEventListener("click", fetchData);

async function fetchData(e) {
    e.preventDefault();
    
    const divTableList = document.getElementById("tableList");
    try {
        const token = localStorage.getItem('token');
        const apiResponse = await fetch('/api/reservation', {
            method: 'GET', // GET-Request
            headers: {
                'Authorization': `Bearer ${token}`, // Bearer Token hinzufügen
              
            },
        });
        const responseText = await apiResponse.text();
        
        try {
            const data = JSON.parse(responseText);
            console.log(data);

            const table = document.createElement("table");
            const headerRow = document.createElement("tr");
            headerRow.innerHTML = '<th>Restaurant</th><th>Adresse</th><th>Raum</th><th>Tisch</th><th>Plaetze</th>';
            table.appendChild(headerRow);

            data.forEach(restaurant => {
                restaurant.rooms.forEach(room => {
                    room.tables.forEach(tableData => {
                        const row = document.createElement("tr");
                        row.innerHTML = `<td>${restaurant.name}</td><td>${restaurant.address}</td><td>${room.roomNumber}</td><td>${tableData.tableID}</td><td>${tableData.chairs}</td>`;
                        table.appendChild(row);
                    });
                });
            });
            divTableList.replaceChild(table, divTableList.firstChild);
        } catch (jsonError) {
            console.error("Error parsing JSON: ", jsonError);
            console.error("Response text: ", responseText);
            divTableList.textContent = "Error fetching table data. Please try again later.";
        }
    } catch (error) {
        console.error("Error fetching table data: ", error);
        divTableList.textContent = "Error fetching table data. Please try again later.";
    }
}