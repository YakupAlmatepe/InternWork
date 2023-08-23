
///
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import React, { useEffect, useState } from "react";

const App = () => {
    const [connection, setConnection] = useState();
    const [inputText, setInputText] = useState([]);

    useEffect(() => {
        getData();

        const connect = new HubConnectionBuilder()
            .withUrl("http://localhost:5000/myhub")
            .withAutomaticReconnect() // baðlantý var ancak koptuðu anlarda kullanýlýr
            .build();

        setConnection(connect);
    }, []);

    useEffect(() => {
        if (connection) {
            connection
                .start()
                .then(() => {
                    connection.on("ReceiveMessage", (message) => {
                        console.log(message)
                        getData();
                    });
                    connection.on("ReceiveVehicle", (message) => {
                        console.log(message)
                        getData();
                    });
                })
                .catch((error) => console.log(error));
        }
    }, [connection]);


    const getData = () => {
        // API isteði atýlýp son durum çekilecek
        fetch("http://localhost:5000/api/Values/getDataFromAPI", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        })
            .then((response) => response.json())
            .then((data) => {
                // API'den gelen sonuçlarý kullan, örneðin:
                console.log("API'den gelen sonuçlar:", data);
                // Sonuçlarý kullanmak için burada uygun iþlemleri yapabilirsiniz.
                setInputText(data); // Örnek olarak gelen mesajý inputText olarak ayarladýk
            })
            .catch((error) => {
                console.error("API isteği sırasında hata oluştu:", error);

            });
    };

    return (
        <>
            {
                inputText.map((row, index) => (
                    <p key={row.id}>{row.tempreture}</p>
                )
                )
            }
        </>
    );
};

export default App;
