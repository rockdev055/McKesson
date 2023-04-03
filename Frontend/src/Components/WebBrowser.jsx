import React, { useEffect, useState, useCallback } from "react";
import "../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "../../node_modules/bootstrap-icons/font/bootstrap-icons.css";
import SearchApi from "../Api/SearchApi";
import ResultCard from "./ResultCard";

export default function WebBrowser() {
  const subsKey = "6c2e93e774ba451685e0ad76571c1038";
  let queryInput = React.createRef();
  const [searchResults, SetsearchResults] = useState([]);
  const [subscriptionKey, SetsubscriptionKey] = useState("");

  const getResults = useCallback(
    (query) => {
      SearchApi.GetResults(query, subscriptionKey)
        .then((response) => {
          if (!response.hasOwnProperty("data")) {
            SetsearchResults([]);
            return;
          }
          const results = response.data?.webPages?.value;
          SetsearchResults(results);
        })
        .catch((error) => {
          alert(error.message);
        });
    },
    [subscriptionKey]
  );

  const getSubscriptionKey = useCallback(() => {
    let key = subsKey;
    while (key.length !== 32) {
      key = prompt("Enter Bing Search API subscription key:", "").trim();
    }
    SetsubscriptionKey(key);
    getResults();
  }, [getResults]);

  useEffect(() => {
    getSubscriptionKey();
  }, [getSubscriptionKey]);

  const handleSearchClick = () => {
    const query = queryInput.current.value;
    getResults(query);
  };

  return (
    <>
      <div className="mt-3">
        <h2>Bing Web Search</h2>
      </div>
      <div className="input-group mb-3">
        <div className="input-group-prepend">
          <span className="input-group-text" id="basic-addon1">
            <i className="bi bi-search"></i>
          </span>
        </div>
        <input
          onKeyDown={(e) => e.key === "Enter" && handleSearchClick()}
          ref={queryInput}
          type="text"
          className="form-control"
          placeholder="Search here!"
          aria-label="Username"
          aria-describedby="basic-addon1"
        ></input>
        <button
          className="btn btn btn-primary"
          type="button"
          id="button-addon2"
          onClick={handleSearchClick}
        >
          Search
        </button>
      </div>
      <div>
        <div className="row row-cols-1 row-cols-md-1 g-1  mt-4">
          {searchResults.length ? (
            searchResults.map((result) => (
              <div key={result.url} className="col">
                <ResultCard dataResult={result}></ResultCard>
              </div>
            ))
          ) : (
            <div className="alert alert-warning" role="alert">
              Not results yet!
            </div>
          )}
        </div>
      </div>
    </>
  );
}
