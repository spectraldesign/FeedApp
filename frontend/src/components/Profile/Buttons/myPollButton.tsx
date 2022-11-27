import { NavLink } from "@solidjs/router";
import "../../button.css"

function MyPollButton() {
    return (
        <div class="button mypolls">
            <NavLink href="/profile/polls">My polls</NavLink>
        </div>
    );
  }

  export default MyPollButton;

