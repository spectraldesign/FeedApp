import { render } from 'solid-js/web';
import { createSignal, Show } from 'solid-js';
import "./login.css";

function Login() {
  const [loggedIn, setLoggedIn] = createSignal(false);
  const toggle = () => setLoggedIn(!loggedIn());
  
  return (
    <>
      <div class='login-btn'>
        {/* <button onClick={toggle}>Log out</button> */}
        <a href="/" onClick={toggle}>&#9760; Log in</a>
        {/* <a href="/" onClick={toggle}>Log out</a> */}
      </div>
    </>
  );
}

export default Login;
