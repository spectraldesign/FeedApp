import "../../button.css";
import { NavLink } from '@solidjs/router';

function ProfileButton() {
  return (
    <>
      <div class='button profile'>
        <NavLink href='/profile' class="testertester"><i class='fas fa-user-alt' style='font-size:15px;color:#c2c7cc;'></i> Profile </NavLink>
      </div>
    </>
  );
}

export default ProfileButton;
