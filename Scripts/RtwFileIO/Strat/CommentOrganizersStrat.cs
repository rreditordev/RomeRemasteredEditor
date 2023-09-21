public static class CommentOrganizersStrat
{
    //For the very Top of the file
    public static string HeaderComments = 
@";------------------------------------------------- TABLE OF CONTENTS
; 1. Playable Factions
; 2. Wonders -- Landmarks Section
; 3. Resources
; 4. Factions
; ------ 4a. Rome
; ------ 4b. Macedon
; ------ ------ 4bA. Macedon in Macedon
; ------ ------ 4bB. Macedon in Greece
; ------ ------ 4bC. Macedon in Asia Minor
; ------ 4c. Ptolemaic
; ------ ------ 4cA. Ptolemaic in Egypt
; ------ ------ 4cB. Ptolemaic in Ethiopia
; ------ ------ 4cB. Ptolemaic in Levant
; ------ ------ 4cD. Ptolemaic in Asia Minor
; ------ ------ 4cE. Ptolemaic in Greece
; ------ 4d. Seleucid
; ------ ------ 4dA. Seleucid in Levant
; ------ ------ 4dB. Seleucid in Asia Minor
; ------ ------ 4dC. Seleucid in Thrace
; ------ ------ 4dD. Seleucid in Mesopotamia
; ------ ------ 4d. Seleucid in Persia
; ------ 4e. Carthage
; ------ ------ 4eA. Carthage in Africa
; ------ ------ 4eB. Carthage in Iberia
; ------ ------ 4eC. Carthage in Sicily
; ------ ------ 4eD. Carthage in Corsica and Sardinia
; ------ 4f.  Parthia
; ------ 4g.  Gauls (Arverni)
; ------ 4h.  Germans
; ------ 4i.  Pontus
; ------ 4j.  Armenia
; ------ 4k.  Dacia
; ------ 4l.  Scythia
; ------ 4m.  Spain (Arevaci)
; ------ 4n.  Thrace (Odrysian Kingdom )
; ------ 4o.  Numidia
; ------ 4p.  Britons (Trinovantes )
; ------ 4q.  Sparta
; ------ 4r.  Galatians
; ------ 4s.  Pergamon
; ------ 4t.  Bosporan
; ------ 4u.  Massalia
; ------ 4v.  Syracuse
; ------ 4w.  Rhodes
; ------ 4x.  Athens
; ------ 4y.  Achaea
; ------ 4z.  Aetolia
; ------ 4aa. Boeotia
; ------ 4ab. Epirus
; ------ 4ac. Bactria
; ------ 4ad. Cyrene
; ------ 4ae. Saka
; ------ 4af. Lusitani
; ------ 4ag. Edetani
; ------ 4ah. Numidia_Occidentalis
; ------ 4ai. Volcae
; ------ 4aj. Allobroges
; ------ 4ak. Aedui
; ------ 4al. Belgae
; ------ 4am. Insubres
; ------ 4an. Boii
; ------ 4ao. Scordisci
; ------ 4ap. Tylis
; ------ 4aq. Bithynia
; ------ 4ar. Chatti
; ------ 4as. Lugii
; ------ 4at. Cimbri
; ------ 4au. Ardiaei
; ------ 4av. Cappadocia
; ------ 4aw. Atropatene
; ------ 4ax. Dummies
; 5. Rebel Towns
; ------ 5a.  Rebel Towns in Italy
; ------ 5b.  Rebel Towns in Sicily
; ------ 5c.  Rebel Towns in Sardinia and Corsica
; ------ 5d.  Rebel Towns in Africa
; ------ ------ 5dA. Rebel Towns in Numidia
; ------ ------ 5dB. Rebel Towns in Phonecian coast
; ------ 5e.  Rebel Towns in Iberia
; ------ ------ 5eA. Rebel Towns in Celt Iberia
; ------ 5f.  Rebel towns in gaul
; ------ ------ 5fA. Rebel towns in Elusatia
; ------ ------ 5fB. Rebel towns in Belgica
; ------ 5g.  Rebel towns in Cisalpine gaul
; ------ ------ 5gA. Rebel towns in Genuatea
; ------ 5h.  Rebel towns in Central Europe
; ------ 5i.  Rebel towns in Br*tain
; ------ ------ 5iA. Rebel towns in Caledonia and Hibernia
; ------ 5j.  Rebel towns in germania
; ------ ------ 5jA. Rebel towns in scandinavia
; ------ ------ 5jB. Rebel towns in Baltic
; ------ ------ 5jC. Rebel towns in Finno-Ugric (OK a howl)
; ------ ------ 5jD. Rebel towns in Hercynia
; ------ 5k.  Rebel towns in Western balkans
; ------ ------ 5kA. Rebel towns in Pannonia
; ------ ------ 5kB. Rebel towns in Illyria
; ------ 5l.  Rebel towns in Eastern balkans
; ------ ------ 5lA. Rebel towns in Dacia
; ------ ------ 5lB. Rebel towns in Thrace
; ------ 5m.  Rebel towns in Greece
; ------ ------ 5mA. Rebel towns in Macedon
; ------ ------ 5mB. Rebel towns in Epirus
; ------ 5n.  Rebel towns in Steppes
; ------ ------ 5nA. Rebel towns in Bosporus
; ------ 5o.  Rebel towns in Asia Minor
; ------ 5p.  Rebel towns in Levant
; ------ 5q.  Rebel towns in Egypt
; ------ 5r.  Rebel towns in Nubia and Ethiopia
; ------ 5s.  Rebel towns in Arabia
; ------ 5t.  Rebel Towns in CAUCASUS
; ------ 5u.  Rebel towns in Mesopotamia
; ------ 5v.  Rebel towns in Persia
; ------ 5w.  Rebel towns in Bactria
; ------ 5x.  Rebel towns in India
; ------ 5y.  Rebel towns in East Steppes
; ------ 5z.  Rebel towns in Tarim Basin
; ------ 5aa. Rebel towns in Terra Incognita
; 6. Rebel Armies
; ------ 6a.  Rebel Armies in Italy
; ------ 6b.  Rebel Armies in Sicily
; ------ 6c.  Rebel Armies in Sardinia and Corsica
; ------ 6d.  Rebel Armies in Africa
; ------ ------ 6dA. Rebel Armiess in Numidia
; ------ ------ 6dB. Rebel Armies in Phonecian coast
; ------ 6e.  Rebel Armies in Iberia
; ------ 6f.  Rebel Armies in gaul
; ------ ------ 6fA. Rebel armies in Elusatia
; ------ ------ 6fB. Rebel armies in Belgica
; ------ 6g.  Rebel Armies in Cisalpine gaul
; ------ ------ 6gA. Rebel armies in Genuatea
; ------ 6h.  Rebel Armies in Central Europe
; ------ 6i.  Rebel Armies in Br*tain
; ------ ------ 6B. Rebel armies in Caledonia and Hibernia
; ------ 6j.  Rebel Armies in germania
; ------ ------ 6jA. Rebel armies in scandinavia
; ------ ------ 6jB. Rebel armies in Baltic
; ------ ------ 6jC. Rebel armies in Finno-Ugric (OK a howl)
; ------ ------ 6jD. Rebel armies in Hercynia
; ------ 6k.  Rebel Armies in Western balkans
; ------ ------ 6kA. Rebel armies in Pannonia
; ------ ------ 6kB. Rebel armies in Illyria
; ------ 6l.  Rebel Armies in Eastern balkans
; ------ ------ 6lA. Rebel armies in Dacia
; ------ ------ 6lB. Rebel armies in Thrace
; ------ 6m.  Rebel Armies in Greece
; ------ ------ 6mA. Rebel armies in Macedon
; ------ ------ 6mB. Rebel armies in Epirus
; ------ 6n.  Rebel Armies in Steppes
; ------ ------ 6nA. Rebel armies in Bosporus
; ------ 6o.  Rebel Armies in Asia Minor
; ------ 6p.  Rebel Armies in Levant
; ------ 6q.  Rebel Armies in Egypt
; ------ 6r.  Rebel Armies in Nubia and Ethiopia
; ------ 6s.  Rebel Armies in Arabia
; ------ 6t.  Rebel Armies in CAUCASUS
; ------ 6u.  Rebel Armies in Mesopotamia
; ------ 6v.  Rebel Armies in Persia
; ------ 6w.  Rebel Armies in Bactria
; ------ 6x.  Rebel Armies in India
; ------ 6y.  Rebel Armies in East Steppes
; ------ 6z.  Rebel Armies in Tarim Basin
; ------ 6aa. Rebel Armies in Terra Incognita";

public static string landmarkComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of landmarks section <<<<";

    public static string resourceComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of resources section <<<<   ! sorted alphabetically !
; slaves - salt now
; furs - horses now
; pigs - fish now
; consult the spreadsheet if needed!! https://docs.google.com/spreadsheets/d/18dkGUL_S1N107XSt6Ea4aPCRga_NS2CXT9h2hN-o79s/edit?usp=sharing
; Resource		type,						quantity,	   X,           Y		; Region Name / Sea / Water";


    public static string soundEmittersComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of sound emitters section <<<<";

    public static string eventsComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of events section <<<<";
    
    public static string factionComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of factions section <<<<";

    public static string diplomacyComments =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of diplomacy section <<<<
;DS_ALLIED			= 0
;DS_SUSPICIOUS		= 100
;DS_NEUTRAL			= 200
;DS_HOSTILE			= 400
;DS_AT_WAR			= 600";

    public static string regionsSection =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> start of regions section <<<<";

    public static string scriptsSection =
@";-------------------------------------------------
;----- DO NOT PLACE SPAWN SCRIPTS BELOW HERE -----
;-------------------------------------------------

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> background scripts <<<<";

    public static string spawnScriptsSection =
@";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; >>>> slave spawn script (to overrule shadow factions if desired) <<<<
; >>>> NOTE: SLAVE SPAWN SCRIPT SHOULD ALWAYS BE THE LAST SPAWN SCRIPT IN LINE! <<<<
; >>>> spawn scripts <<<<";
}
