import { Provider } from "react-redux";
import "../CSS/App.css";
import store from "../store";
import Navbar from "./Navbar";

function App() {
  return (
    <Provider store={store}>
      <header className="App-header">
        <Navbar />
      </header>
    </Provider>
  );
}

export default App;
