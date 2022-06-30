import Home from "./Pages/Home";
import Footer from "./Pages/Footer";
import Account from "./Pages/Account";
import Cards from "./Pages/Cards";
import CardCreate from "./Pages/CardCreate";
import CardChange from "./Pages/CardChange";
import Card from "./Pages/Card";
import Login from "./Pages/Login";
import Registration from "./Pages/Registration";
import Navbar from "./Pages/Components/Navbar";
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <>
      <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/account" element={<Account />} />
          <Route path="/cards" element={<Cards />} />
          <Route path="/cards" element={<Cards />} />
          <Route path="/cardCreate" element={<CardCreate />} />
          <Route path="/cardChange/:id" element={<CardChange />} />
          <Route path="/card/:id" element={<Card />} />
          <Route path="/login" element={<Login />} />
          <Route path="/regestration" element={<Registration />} />
        </Routes>
        <Footer/>

    </>
  );
}

export default App;
