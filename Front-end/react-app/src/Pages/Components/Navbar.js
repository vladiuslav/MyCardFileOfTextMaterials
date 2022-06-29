import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="nav">
      <ul>
        <CustomLink to="/">Home</CustomLink>
        <CustomLink to="/cards">Cards</CustomLink>
        {CheckEmail()}
      </ul>
    </nav>
  );
}

function CheckEmail() {
  if (sessionStorage.getItem("Email") == null) {
    return (
      <div>
        <CustomLink to="/login">Login</CustomLink>
        <CustomLink to="/regestration">Regestration</CustomLink>
      </div>
    );
  } else {
    return (
      <div>
        <CustomLink to="/cardCreate">Create card</CustomLink>
        <CustomLink to="/account">Account</CustomLink>
        <LogOut />
      </div>
    );
  }
}
function LogOut(props) {
  let navigation = useNavigate();
  return (
    <li>
      <p
        onClick={() => {
          sessionStorage.clear();
          navigation("/");
          window.location.reload(false);
        }}
      >
        LogOut
      </p>
    </li>
  );
}

function CustomLink({ to, children, ...props }) {
  return (
    <li>
      <Link to={to} {...props}>
        {children}
      </Link>
    </li>
  );
}
