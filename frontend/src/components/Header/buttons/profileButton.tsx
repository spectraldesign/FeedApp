import "../../button.css";
import { NavLink } from '@solidjs/router';

function ProfileButton() {
  return (
    <>
      <div class='button profile'>
      <a href="/profile">Profile</a>
      </div>
    </>
  );
}

export default ProfileButton;
