import { render } from 'solid-js/web';
import { createSignal, Show } from 'solid-js';
import "./profileButton.css";
import { NavLink } from '@solidjs/router';

function ProfileButton() {
//   const [loggedIn, setLoggedIn] = createSignal(false);
//   const toggle = () => setLoggedIn(!loggedIn());
  
  return (
    <>
      <div class='login-btn'>
        {/* <button onClick={toggle}>Log out</button> */}
        <NavLink href='/profile' class="testertester"><i class='fas fa-user-alt' style='font-size:15px;color:#c2c7cc;'></i> Profile </NavLink>
        {/* <i class='fas fa-user-alt' style='font-size:10px;color:white;'>Profile</i> */}
        {/* <a href="/" onClick={toggle}>Log out</a> */}
      </div>
    </>
  );
}

export default ProfileButton;
