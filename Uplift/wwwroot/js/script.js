function swapPic1() {
    $('#si-main-image').attr('src', $('#p1').attr('src'));
    
}
function swapPic2() {
    $('#si-main-image').attr('src', $('#p2').attr('src'));
}
function swapPic3() {
    $('#si-main-image').attr('src', $('#p3').attr('src'));
}



function executeSearchQuery() {
    var searchQuery = document.getElementById("search-query-input-box").value;
    structured_search_query = searchQuery.replace(/\s+/g, '-').toLowerCase();
    newLoc = "https://xchangewebsite.azurewebsites.net/Customer/Results/Index/all/r/" + structured_search_query;
    window.location = newLoc;
}


$('#search-query-input-box').keypress(function (e) {
    if (e.keyCode == 13) {
        executeSearchQuery();
        e.preventDefault();
    }
});

document.getElementById("search-query-submit-button").onclick = executeSearchQuery;



// FIX THIS FUNCTION ( PUT LOGIC IN IT ):
function sortResults() {
    var p = document.createElement("p");
    p.textContent = document.getElementById("sort-results-dropdown").value;
    document.getElementById("Search-results-items-div").appendChild(p);
}
document.getElementById("sort-results-dropdown").onchange = sortResults;

function loadOffer(offerFName, offerLName, offerBuyerGuid) {

    var tr = document.createElement('tr');
    var th = document.createElement('th');
    var thAtt = document.createAttribute('scope');
    thAtt.value = 'row';
    th.setAttributeNode(thAtt);

    var userLink = document.createElement('a');
    linkAtt = document.createAttribute('href');

    // CHANGE THIS LINE ONCE WE GET GUID:
    linkAtt.value = "mailto:" + offerBuyerGuid.toString();
    userLink.setAttributeNode(linkAtt);
    userLink.textContent = offerFName + " " + offerLName;

    th.appendChild(userLink);
    tr.appendChild(th);

    var tableBody = document.getElementById("offers-table-body");
    tableBody.appendChild(tr)
}


function loadItem(itemTitle, itemDesc, itemPrice, parentDiv, itemID) {

    var divCol = document.createElement('div');
    var divAtt = document.createAttribute('class');
    divAtt.value = 'col-md-6 col-lg-3 d-flex align-items-stretch';
    divCol.setAttributeNode(divAtt);
    divAtt = document.createAttribute('data-aos');
    divAtt.value = 'fade-up';
    divCol.setAttributeNode(divAtt);
    divAtt = document.createAttribute('data-aos-delay');
    divAtt.value = '200';
    divCol.setAttributeNode(divAtt);

    var itemLink = document.createElement('a');
    linkAtt = document.createAttribute('href');
    linkAtt.value = 'https://xchangewebsite.azurewebsites.net/Customer/Items/Index/' + itemID.toString();
    itemLink.setAttributeNode(linkAtt);
    linkAtt = document.createAttribute('style');
    linkAtt.value = "color:inherit;";
    itemLink.setAttributeNode(linkAtt);

    var divBox = document.createElement('div');
    divAtt = document.createAttribute('class');
    divAtt.value = 'icon-box icon-box-blue';
    divBox.setAttributeNode(divAtt);

    var divImage = document.createElement('div');

    var img = document.createElement("img");
    imgAtt = document.createAttribute('src');
    // REPLACE THE VALUE AFTER /GetItemPhoto/ with a call to var ItemID above
    // Add this: https://xchangewebsite.azurewebsites.net/
    imgAtt.value = 'https://xchangewebsite.azurewebsites.net/Customer/Newlisting/GetItemPhoto/' + itemID.toString();
    img.setAttributeNode(imgAtt);

    imgAtt = document.createAttribute('alt');
    imgAtt.value = '';
    img.setAttributeNode(imgAtt);

    imgAtt = document.createAttribute('style');
    imgAtt.value = 'max-width:100px;max-height:100px;margin-bottom:10px';
    img.setAttributeNode(imgAtt);

    var h4Title = document.createElement("h4");
    h4Att = document.createAttribute('class');
    h4Att.value = 'title';
    h4Title.setAttributeNode(h4Att);
    h4Title.textContent = itemTitle;


    var price = document.createElement("p");
    priceAtt = document.createAttribute('style');
    priceAtt.value = 'color:grey;';
    price.setAttributeNode(priceAtt);
    price.textContent = '$' + itemPrice.toString();

    if (itemDesc.length > 100) {
        itemDesc = itemDesc.substring(0, 100) + "...";
    }

    var desc = document.createElement("p");
    descAtt = document.createAttribute('class');
    descAtt.value = 'description';
    desc.setAttributeNode(descAtt);
    desc.textContent = itemDesc;

    divImage.appendChild(img);
    divBox.appendChild(divImage);
    divBox.appendChild(h4Title);
    divBox.appendChild(price)
    divBox.appendChild(desc);
    itemLink.appendChild(divBox);
    divCol.appendChild(itemLink);

    var div = document.getElementById(parentDiv);
    div.appendChild(divCol);
}