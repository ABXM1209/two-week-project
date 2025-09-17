
import './App.css'
import {finalUrl} from "./BaseUrl.ts";

function App() {
    

  return (
    <>
        <button onClick={()=>{
            fetch(finalUrl)
                .then(response =>{
                    console.log(response)
                }).catch(error =>{
                    console.log(error)
            })
        }}>OI OVER HERE CLICK ME MATE</button>
    </>
  )
}

export default App
