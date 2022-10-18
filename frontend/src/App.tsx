import type { Component } from 'solid-js';
import Header from './components/Header/header';
import SearchPoll from './components/Poll/Poll-Search/searchPoll';

const App: Component = () => {
  return (
    <>
      <Header />
      <SearchPoll />
    </>
  );
};

export default App;
