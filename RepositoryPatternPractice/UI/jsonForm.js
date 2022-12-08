const apiUrl="https://localhost:44396/api/ProductAPI/HtmlForm"; 

async function fetchPost(){
    try{
    const response= await fetch(`${apiUrl}`);

    if(!response.ok){
        throw new Error(`Failed to Fetch Response ${response.status}`);
    }
    return response.json();
    }catch(e){
        console.log(e);
    }
}


function listPosts(postContainerElementId){
            

    const postContainerElement= document.getElementById(postContainerElementId);
    if(!postContainerElement){
        return;
    }

    fetchPost()
    .then((posts) => {

        if(!posts){
            postContainerElement.innerHTML="No posts";
            return;
        }

        
        //respponse to json string obj
        var jObjString = JSON.stringify(posts);
        // json obj string to json obj
        var jObject = JSON.parse(jObjString)
        // object...
        var form=jObject.productName;

        postContainerElement.innerHTML=form;
    
    })
    .catch((e) => {
        console.log(e);
    });
}
