import React, { useState, useEffect } from 'react';

function App() {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {

    (async function() {
      let content = await fetch(`/api/TempApi`);

      let message = content.status < 300 ? (await content.text()) : `Error=${content.status}, ${content.statusText}`;
      
      setData(message);

    })();

      // fetch(`/api/TempApi`)
      // .then((response) => console.log(response.text()))
      // .then((actualData) => console.log(actualData))
      // .catch((error) => {
      //   console.log(error.message);
      // });

  });

  return <>
      <div>
        {data}
      </div>
    </>
}

export default App;