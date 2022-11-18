import { render } from 'solid-js/web';
import { createSignal, Show } from 'solid-js';
import "../../button.css";
import { NavLink } from '@solidjs/router';

function Login() {
  const [loggedIn, setLoggedIn] = createSignal(false);
  const toggle = () => setLoggedIn(!loggedIn());
  
  return (
    <>
      <div class='button login'>
        <NavLink href='/login' onClick={toggle}>&#9760; Log in </NavLink>
      </div>
    </>
  );
}

export default Login;
