import React, { useState, useEffect } from 'react';

function App() {
    const [currentTemp, setCurrentTemp] = useState(null);
    const [averageTemp, setAverageTemp] = useState(null);

    const sectionStyle = {
        backgroundColor: '#336699',
    };

    const cardStyle = {
        borderRadius: '35px',
        color: '#4B515D',
    };

    const tempStyle = {
        color: '#1C2331'
    };

    const descStyle = {
        color: '#868B94'
    };

    useEffect(() => {

        (async function () {
            let content = await fetch(`/api/TempApi`);

            let message = content.status < 300 ? (await content.text()) : `Error=${content.status}, ${content.statusText}`;
            
            let data = await JSON.parse(message)

            setCurrentTemp(data.currentTemperature.toFixed(1))
            setAverageTemp(data.averageTemperature.toFixed(1))

        })();

    }, []);

    return <>
<section class="vh-100" style={sectionStyle}>
  <div class="container py-5 h-100">

    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-md-8 col-lg-6 col-xl-4">

        <div class="card" style={cardStyle}>
            <div class="card-body p-4">

            <div class="d-flex">
                <h6 class="flex-grow-1">Nackademin</h6>
            </div>

            <div class="d-flex flex-column text-center mt-5 mb-4">
                <h6 class="display-4 mb-0 font-weight-bold" style={tempStyle}> {currentTemp}°C </h6>
                <span class="small" style={descStyle}>Snittvärde på senaste mätningen från SMHI och OpenWeather</span>
            </div>

            <div class="d-flex align-items-center">               
                <div class="flex-grow-1" >
                    <div> 
                        <h6 class="flex-grow-1"> 
                            Medelvärde dom 100 senaste mätningarna: {averageTemp}°C
                        </h6>
                    </div>
                </div>
            </div>

          </div>
        </div>

      </div>
    </div>

  </div>
</section>
    </>
}

export default App;