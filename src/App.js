import React, { useState, useEffect } from 'react';

function App() {
  const [data, setData] = useState(null);

  useEffect(() => {

    (async function() {
      let content = await fetch(`/api/TempApi`);

      let message = content.status < 300 ? (await content.text()) : `Error=${content.status}, ${content.statusText}`;
      
      setData(message);

    })();

  });

  return <>
      <div>
        hej 
        {data}
      </div>
    </>
}

export default App;