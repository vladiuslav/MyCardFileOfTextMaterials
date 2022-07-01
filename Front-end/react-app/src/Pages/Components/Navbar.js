import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="nav">
      <ul>
        <div className="LeftDiv">
          <CustomLink to="/">Home</CustomLink>
          <CustomLink to="/cards">Cards</CustomLink>
          <Categories/>
        </div>
        {CheckEmail()}
      </ul>
    </nav>
  );
}

function Categories(props) {
  if (sessionStorage.getItem("Role") === "admin") {
    return <CustomLink to="/categories">Categories</CustomLink>;
  } else {
    return "";
  }
}

function CheckEmail() {
  if (sessionStorage.getItem("Email") == null) {
    return (
      <div className="RightDiv">
        <CustomLink to="/login">Login</CustomLink>
        <CustomLink to="/regestration">Regestration</CustomLink>
      </div>
    );
  } else {
    return (
      <div className="RightDiv">
        <CustomLink to="/cardCreate">CreateCard</CustomLink>
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
        className="navItem"
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
      <Link className="navItem" to={to} {...props}>
        {children}
      </Link>
    </li>
  );
}
