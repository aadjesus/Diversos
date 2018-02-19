
// config

function $p(e)
{
  return PGPATH + e;
}

// inspired from Prototype, but sterioded

Element =
{
extended: true,

visible: function(vis)
{
  if (vis != null)  {
    if (typeof vis == 'boolean')
      vis = vis ? '' : 'none';
    this.style.display = vis;        
  }
  return this.style.display != 'none';
},

kids: function(index)
{
  if (index == null) {
    var c = [];
    for (var i = 0; i < this.childNodes.length; i++)
      if (this.childNodes[i].nodeType != 3)
        c.push($(this.childNodes[i]));
    return c;
  } 
  else 
  {
    for (var i = 0, j = 0; i < this.childNodes.length; i++) {
      if (this.childNodes[i].nodeType != 3) {
        if (j == index)
          return $(this.childNodes[i]);
        j++;
      }
    }
    return null;
  }
},

parent: function()
{
  return $((this.parentNode == 'undefined') ? this.parentElement : this.parentNode);
},

prev: function()
{
  var p = this.previousSibling;
  while (p.nodeType == 3)
    p = p.previousSibling;
  return $(p);
},

next: function()
{
  var p = this.nextSibling;
  while (p.nodeType == 3)
    p = p.nextSibling;
  return $(p);
}
};

function $(e) 
{
  function extend(dst,src) 
  {
    if (!dst.extended) 
      for (var i in src) 
        dst[i] = src[i];
    return dst;
  }
  return extend( (typeof e == 'string') ?  document.getElementById(e) : e , Element);
}

// from Anthem (renamed to Skinny)

Skinny = 
{
GetXMLHttpRequest: function () 
{
	if (window.XMLHttpRequest) 
	{
		return new XMLHttpRequest();
	} 
	else 
	{
		if (window.Skinny_XMLHttpRequestProgID) 
		{
			return new ActiveXObject(window.Skinny_XMLHttpRequestProgID);
		} 
		else 
		{
			var progIDs = ["Msxml2.XMLHTTP", "Microsoft.XMLHTTP"];
			for (var i = 0; i < progIDs.length; ++i) 
			{
				var progID = progIDs[i];
				try 
				{
					var x = new ActiveXObject(progID);
					window.Skinny_XMLHttpRequestProgID = progID;
					return x;
				} 
				catch (e) {	}
			}
		}
	}
	return null;
},

Invoke: function (id, method, args, callBack, state) 
{
	var x = Skinny.GetXMLHttpRequest();
	var result = null;
	if (!x) 
	{
		result = { "value": null, "error": "NOXMLHTTP" };
		if (callBack) 
		{
			callBack(result, state);
		}
		return result;
	}
	x.open("POST", Skinny_DefaultURL, callBack ? true : false);
	x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
	if (callBack) 
	{
		x.onreadystatechange = function() 
		{
			if (x.readyState != 4) 
				return;
			result = Skinny.Result(x);
			callBack(result, state);
			x = null; // IE memleak fix?
		}
	}
  var encodedData = "&SID=" + id.split(":").join("_");
  encodedData += "&SM=" + method;

	if (args) 
	{
		for (var argsIndex = 0; argsIndex < args.length; ++argsIndex) 
		{
			if (args[argsIndex] instanceof Array) 
				for (var i = 0; i < args[argsIndex].length; ++i) 
					encodedData += "&SA" + argsIndex + "=" + encodeURIComponent(args[argsIndex][i]);
			else 
				encodedData += "&SA" + argsIndex + "=" + encodeURIComponent(args[argsIndex]);
		}
	}
	x.send(encodedData);
	return result;
},

Result: function(x) 
{
	var result = { "value": null, "error": null};
	var responseText = x.responseText;
	try 
	{
		result = eval("(" + responseText + ")");
	} 
	catch (e) 
	{
		if (responseText.length == 0) 
			result.error = "NORESPONSE";
		else 
		{
			result.error = "BADRESPONSE";
			result.responseText = responseText;
		}
	}
	return result;
}
};

// start PropertyGrid

var oldval = null;
var oldlbl = null;

function PropertyGrid(id, items, selcolor, itembgcolor, cats,
  width, bgcolor, headerfgcolor, lineheight, fgcolor, family, fontsize, interval, path)
{
  this.id = id;
  this.items = items;
  this.selcolor = selcolor;
  this.itembgcolor = itembgcolor;
  this.cats = cats;
  this.width = width;
  this.bgcolor = bgcolor;
  this.bordercolor = headerfgcolor;
  this.headerfgcolor = headerfgcolor;
  this.lineheight = lineheight;
  this.padwidth = 18;
  this.fgcolor = fgcolor;
  this.family = family;
  this.fontsize = fontsize;
  this.interval = interval;
  PGPATH = path;
}

PropertyGrid.prototype =
{
currentedit : null,
endeditlock : null,
timerid     : null,
livemode    : false,
viewlevel   : 2,

ApplyStyles: function(stylesheet)
{
  var self = this;
  function rule(sel,val)
  {
    var sels = sel.split(',');
    for (var i = 0; i < sels.length; i++)
    {
      var s = sels[i];
      var re = /\s/;
      var res = re.exec(s);
      if (res)
        s = s.replace(re, '_' + self.id + ' ');
      else
        s = s + '_' + self.id;
      if (stylesheet.addRule) //IE
        stylesheet.addRule(s, val);
      else if (stylesheet.insertRule) // Moz
        stylesheet.insertRule(s + '{' + val + '}', stylesheet.cssRules.length);
      else
        return; //opera
    }
  }
  rule('.PG'                                ,'width:' + this.width + 'px');
  rule('.PG *'                              ,'color:' + this.fgcolor + ';font-family:' + this.family + ';font-size:' + this.fontsize);
  rule('.PGH,.PGF,.PGC,.PGF2'               ,'border-color:' + this.headerfgcolor + ';background-color:' + this.bgcolor);
  rule('.PGC *'                             ,'line-height:' + this.lineheight + 'px;height:' + this.lineheight +'px');
  rule('.PGC a,.PGC_OPEN,.PGC_CLOSED'       ,'width:' + this.padwidth + 'px');
  rule('.PGC_HEAD span'                     ,'color:' + this.headerfgcolor);
  rule('.PGI_NONE,.PGI_OPEN,.PGI_CLOSED'    ,'width:' + this.padwidth + 'px;height:' + this.LineHeightMargin() + 'px');
  rule('.PGI_NAME,.PGI_VALUE,.PGI_NAME_SUB' ,'width:' + this.HalfWidth() + 'px;background-color:' + this.itembgcolor);
  rule('.PGI_VALUE a,.PGI_VALUE select'     ,'width:100%');
  rule('.PGI_NAME_SUB span'                 ,'margin-left:' + this.padwidth + 'px');
  rule('.PGI_VALUE a:hover'                 ,'background-color:' + this.selcolor);
  rule('.PGI_VALUE input'                   ,'width:' + this.HalfWidthLess3() + 
    'px;line-height:' + this.InputLineHeight() + 'px;height:' + this.InputLineHeight() + 'px');
},

WidthInner:       function(){return this.width - 2;},
LineHeightMargin: function(){return this.lineheight + 1;},
WidthLessPad:     function(){return this.WidthInner() - this.padwidth;},
HalfWidth:        function(){return this.WidthLessPad()/2;},
HalfWidthLess3:   function(){return this.HalfWidth() - 5;},
InputLineHeight:  function(){return this.lineheight - 4;},

ShowHelp: function(sender)
{
  if (this.currentedit)
    this.EndEdit(this.currentedit);
  
  var s = $(sender).next().firstChild.firstChild;
  this.ToggleActivity(true);
  
  var self = this;
  Skinny.Invoke(this.id, 'GetDescription', [s.id.substr(this.id.length + 1)], 
    function(a) { self.UpdateDescription(a); }, null);
},

HandleKey: function(sender,e)
{
  if (e.keyCode == 13) //enter
  {
    this.EndEdit(sender);
    return false;
  }
  if (e.keyCode == 27) //escape
  {
    this.CancelEdit(sender);
    return false;
  }
  return true;
},

BeginEdit: function(sender)
{
  if (this.currentedit)
    this.EndEdit(this.currentedit);
  
  sender = $(sender);
  this.endeditlock = null;
  var s = sender.firstChild;
  this.ToggleActivity(true);
  
  var self = this;
  Skinny.Invoke(this.id, 'GetDescription', [s.id.substr(this.id.length + 1)], 
    function(a) { self.UpdateDescription(a); }, null);
  
  sender.visible(false);
  sender.parent().prev().style.backgroundColor = this.selcolor;
  var edit = sender.next();
  edit.visible(true);
  
  if (edit.nodeName == 'SELECT')
  {
    var seltext = s.innerHTML;
    var kids = edit.kids();
    for (var i = 0; i < kids.length; i++)
      kids[i].selected = kids[i].value == seltext;
  }
  else
    edit.value = s.innerHTML;
  
  edit.focus();
  
  this.currentedit = edit;
},

EndEdit: function(sender)
{
  if (this.endeditlock)
    return;
  
  sender = $(sender);
  this.ToggleActivity(true);
  this.endeditlock = sender;
  this.currentedit = null;
  sender.parent().prev().style.backgroundColor = this.itembgcolor;
  sender.visible(false);

  var label = sender.prev();
  label.visible(true);

  var newval = (sender.nodeName == 'SELECT') ?
    sender.kids(sender.selectedIndex).innerHTML :
    sender.value;

  var firstchild = label.firstChild;
  oldlbl = firstchild;
  oldlbl.disabled = true;
  oldval = firstchild.innerHTML;
  firstchild.innerHTML = newval;
  
  var self = this;
  Skinny.Invoke(this.id, 'SetValue', [firstchild.id.substr(this.id.length + 1), newval], 
    function(a) { self.SetValue(a); }, null);
},

CancelEdit: function(sender)
{
  if (this.currentedit == null)
    return;
  
  sender = $(sender);
  this.currentedit = null;
  this.endeditlock = sender;

  sender.parent().prev().style.backgroundColor = this.itembgcolor;
  sender.visible(false);
  var label = sender.prev();
  
  label.visible(true);
  this.UpdateDescription();
},

ToggleActivity: function(active)
{
  if (this.endeditlock)
    return;

  var ae = $(this.id + '_active');
  ae.visible(active);
},

UpdateDescription: function(result)
{
  if (result && result.error)
  {
    this.ToggleActivity(false);
    return;
  }
  var ft = $(this.id + '_foot');
  if (result == null )
    result = {'value':['An ASP.NET PropertyGrid','(c) 2006 leppie']};

  ft.innerHTML = '<span style="font-weight:bold;display:block">' + result.value[0] + '</span>' + result.value[1];
  this.ToggleActivity(false);
},

GetValues: function(sender)
{
  if (oldlbl == null && this.currentedit == null)
  {
    if (sender != null)
    {
      sender.src = $p('unfresh.gif');
      sender.disabled = true;
    }
    if (this.livemode ^ sender != null)
    {
      this.ToggleActivity(true);
      var self = this;
      Skinny.Invoke(this.id, 'GetValues', [], function(a,b) { self.UpdateValues(a,b); }, sender);
    }
  }
},

UpdateValues: function(result,sender)
{
  var vals = result.value;
  if (vals.length != this.items.length)
  {
    window.location = window.location;
    return;
  }
  for (var i = 0; i < vals.length; i++)
  {
    var lbl = $(this.id + '_' + this.items[i]);
    var val = vals[i];
    if (lbl.innerHTML != val)
    {
      lbl.innerHTML = val;
      var par = lbl.parent();
      par.title = val;
    }
  }
  if (sender != null && !this.livemode)
  {
    sender.src = $p('refresh.gif');
    sender.disabled = false;
  }
  else if (this.livemode && sender == null)
  {
    var self = this;
    this.timerid = setTimeout(function() { self.GetValues(); }, this.interval);
  }
  this.ToggleActivity(false);
  this.UpdateDescription();
},

SetValue: function(result)
{
  this.endeditlock = null;
  if (result.error != null)
  {
    this.ToggleActivity(false);
    alert(result.error);
    oldlbl.innerHTML = oldval;
    oldlbl.disabled = false;
    oldlbl = oldval = null;
    this.UpdateDescription();
    return;
  }
  oldlbl.disabled = false;
  oldlbl = oldval = null;
  this.UpdateValues(result, "FAKE");
},

LiveMode: function(sender)
{
  sender = $(sender);
  this.livemode = !this.livemode;
  if (this.livemode)
  {
    var rf = sender.next();
    rf.src = $p('unfresh.gif');
    rf.disabled = true;
    sender.src = $p('on.gif');
    var self = this;
    this.timerid = setTimeout(function() { self.GetValues(); }, this.interval);
  }
  else
  {
    clearTimeout(this.timerid);
    sender.src = $p('off.gif');
    var rf = sender.next();
    rf.src = $p('refresh.gif');
    rf.disabled = false;
  }
},

SetViewLevel: function()
{
  var level = this.viewlevel;
  var el = $(this.id + '_cats');
  el.visible(level != 0);
  var head = $(this.id).kids(0);
  head.style.borderBottomWidth = (level == 0) ? '0px' : '1px';
  this.ToggleAll(level > 1);
},

ExpandAll: function(sender)
{
  if (this.viewlevel < 2)
    this.viewlevel++;

  sender.src = this.viewlevel == 2 ? $p('expand2.gif') : $p('expand.gif');
  sender = $(sender).next();
  sender.src = this.viewlevel == 0 ? $p('collapse2.gif') : $p('collapse.gif');
  this.SetViewLevel();
},

CollapseAll: function(sender)
{
  if (this.viewlevel > 0)
    this.viewlevel--;

  sender.src = this.viewlevel == 0 ? $p('collapse2.gif') : $p('collapse.gif');
  sender = $(sender).prev();
  sender.src = this.viewlevel == 2 ? $p('expand2.gif') : $p('expand.gif');
  this.SetViewLevel();
},

ToggleAll: function(open)
{
  for (var i = 0; i < this.cats.length; i++)
  {
    var el = $(this.id + '_cat' + this.cats[i]);
    var sender = el.prev().prev();
    PGCatToggle2(sender,el,open);
  }
},

ToggleHelp: function(sender)
{
  sender = $(sender);
  var ft = $(this.id + '_foot').parent();
  var vis = ft.visible();
  sender.src = vis ? $p('helpoff.gif') : $p('help.gif');
  ft.visible(!vis);
}
};

function PGCatToggle(sender) 
{
  var el = $(sender).next().next();
  var open = !el.visible();
  PGCatToggle2(sender,el, open);
}

function PGCatToggle2(sender,el,open) 
{
  el.visible(open);
  sender.className = open ? 'PGC_OPEN' : 'PGC_CLOSED';
}

function PGSubToggle(sender) 
{
  var el = $(sender).parent().next();
  var open = !el.visible();
  PGSubToggle2(sender, el, open);
}

function PGSubToggle2(sender,el,open) 
{
  el.visible(open);
  sender.className = open ? 'PGI_OPEN' : 'PGI_CLOSED';
}

function PGShowInfo()
{
  open('http://blogs.wdevs.com/leppie', '_blank');
}
