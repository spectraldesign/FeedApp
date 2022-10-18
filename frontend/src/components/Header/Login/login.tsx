import { render } from 'solid-js/web';
import { createSignal, Show } from 'solid-js';
import "./login.css";
import { NavLink } from '@solidjs/router';

function Login() {
  const [loggedIn, setLoggedIn] = createSignal(false);
  const toggle = () => setLoggedIn(!loggedIn());
  
  return (
    <>
      <div class='login-btn'>
        {/* <button onClick={toggle}>Log out</button> */}
        <NavLink href='/login' onClick={toggle}>&#9760; Log in </NavLink>
        {/* <a href="/" onClick={toggle}>Log out</a> */}
      </div>
    </>
  );
}

export default Login;
