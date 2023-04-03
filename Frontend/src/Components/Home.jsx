import "../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "../../node_modules/bootstrap-icons/font/bootstrap-icons.css";
import WebBrowser from "./WebBrowser";

export default function Home() {
  return (
    <>
      <div>
        <div className="h-100 p-5 text-white bg-dark border rounded-3">
          <h2>Microsoft Bing Search API</h2>
          <p>Test Assessment</p>
        </div>
      </div>
      <WebBrowser />
    </>
  );
}
