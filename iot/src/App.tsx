import { Component, createEffect, createSignal } from 'solid-js';
import { Routes, Route } from '@solidjs/router';
import Startscreen from './routes/Startscreen';
import Votescreen from './routes/Votescreen';
import axios from 'axios';

//const [id, setId] = createSignal('');

const App: Component = () => {
  //createEffect(async() => {
  //  const res = await axios.get(`https://localhost:7280/api/IoT/${id()}`);
  //  const data = await res.data;
  //  console.log(data);
  //});
  return (
    <>
      <Routes>
        <Route path="/" element={<Startscreen />} />
        <Route path='/id' element={<Votescreen />} />
      </Routes>
    </>
  );
};

//export { id, setId };
export default App;
