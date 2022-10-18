import type { Component } from 'solid-js';
import Homepage from './Pages/Homepage';
import { Routes, Route } from '@solidjs/router';
import MyPollsPage from './Pages/MyPollsPage';
import LoginPage from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';
import ProfilePage from './Pages/ProfilePage';
import VotePage from './Pages/VotePage';
import CreatePollPage from './Pages/CreatePollPage';

const App: Component = () => {
  return (
    <>
      <Routes>
        <Route path="/" element={<Homepage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/polls/:id" element={<VotePage />} />
        <Route path="/profile/polls" element={<MyPollsPage />} />
        <Route path="/poll/create" element={<CreatePollPage />} />
      </Routes>
    </>
  );
};

export default App;
