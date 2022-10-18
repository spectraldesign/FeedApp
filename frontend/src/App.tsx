import type { Component } from 'solid-js';
import Homepage from './components/Pages/Homepage';
import { Routes, Route } from '@solidjs/router';
import MyPollsPage from './components/Pages/MyPollsPage';
import LoginPage from './components/Pages/LoginPage';
import RegisterPage from './components/Pages/RegisterPage';
import ProfilePage from './components/Pages/ProfilePage';
import VotePage from './components/Pages/VotePage';
import CreatePollPage from './components/Pages/CreatePollPage';

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
