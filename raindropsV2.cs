<!DOCTYPE html>
<html lang="en" class="dark">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>CTW Social</title>
<script src="https://cdn.tailwindcss.com"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css">
<style>
  :root { --blue: rgb(12, 22, 162); --green:#00ba7c; --gold:#c8a96e; }
  body { font-family:system-ui,sans-serif; }
  ::-webkit-scrollbar { width:4px; }
  ::-webkit-scrollbar-track { background:#000; }
  ::-webkit-scrollbar-thumb { background:#333; border-radius:4px; }
  .post-card { transition:background .12s; }
  .post-card:hover { background:#080808; }
  .btn-blue { background:var(--blue); transition:background .15s,transform .1s; }
  .btn-blue:hover { background:#1a8cd8; }
  .btn-blue:active { transform:scale(.97); }
  .btn-blue:disabled { opacity:.4; cursor:not-allowed; }
  .sidebar-item:hover { background:#111; }
  .sidebar-item.active { font-weight:700; }
  #toast { transition:opacity .3s,transform .3s; pointer-events:none; }
  #toast.hidden { opacity:0; transform:translateY(20px); }
  .skeleton {
    background:linear-gradient(90deg,#1a1a1a 25%,#222 50%,#1a1a1a 75%);
    background-size:200%; animation:shimmer 1.4s infinite; border-radius:6px;
  }
  @keyframes shimmer { 0%{background-position:200%}100%{background-position:-200%} }
  .avatar { object-fit:cover; background:#222; }
  .char-ring { transition:stroke-dashoffset .2s; }
  .dm-bubble-me   { background: rgb(12, 22, 162); border-radius:1.2rem 1.2rem 0.2rem 1.2rem; }
  .dm-bubble-them { background:#1f2937; border-radius:1.2rem 1.2rem 1.2rem 0.2rem; }
  .cipher-glow { box-shadow:0 0 12px rgba(0,186,124,.18); }

  /* ── FILIGREE DIVIDERS ─────────────────────────────── */
  .filigree-h {
    width:100%; height:32px; overflow:visible;
    flex-shrink:0; display:block;
  }
  .filigree-v {
    position:absolute; right:0; top:0; width:32px; height:100%;
    pointer-events:none; display:block;
  }

  /* ── LOCK OVERLAY ──────────────────────────────────── */
  #lock-overlay {
    display:none;
    position:fixed; inset:0; z-index:195;
    background:rgba(0,0,0,0.88);
    backdrop-filter:blur(22px);
    flex-direction:column;
    align-items:center; justify-content:center; gap:1.5rem;
  }
  #lock-overlay.active { display:flex; }
  #lock-overlay .lock-box {
    background:#06060a;
    border:1px solid #2a2010;
    border-radius:1.5rem;
    padding:2.5rem 3rem;
    display:flex; flex-direction:column; align-items:center; gap:1.2rem;
    min-width:320px; max-width:360px;
    box-shadow:0 0 48px rgba(200,169,110,0.08);
  }
  #lock-overlay .lock-status {
    font-size:.8rem; color:#666; letter-spacing:.08em; text-transform:uppercase;
  }

  /* ── SCREENSAVER SPEED PANEL ───────────────────────── */
  #ss-speed-panel {
    position:absolute; bottom:100%; left:0; right:0;
    background:#0a0a0a; border:1px solid #2a2010;
    border-radius:.75rem; padding:1rem; margin-bottom:.5rem;
    display:none; z-index:50;
  }
  #ss-speed-panel.open { display:block; }
  .speed-btn {
    flex:1; padding:.4rem 0; border-radius:.5rem; font-size:.75rem;
    font-weight:700; background:#111; color:#666;
    border:1px solid #2a2010; cursor:pointer; transition:background .12s,color .12s;
  }
  .speed-btn.active { background:var(--blue); color:#fff; border-color:var(--blue); }
  #ss-speed-toggle {
    width:100%; display:flex; align-items:center; gap:.5rem;
    padding:.6rem .75rem; border-radius:.75rem; font-size:.75rem;
    font-weight:600; color:#555; background:none; border:none;
    cursor:pointer; transition:color .12s;
  }
  #ss-speed-toggle:hover { color:#999; }
</style>
</head>
<body class="bg-black text-white min-h-screen overflow-x-hidden">

<!-- ══ FILIGREE SVG DEFS ═══════════════════════════════════════════ -->
<svg width="0" height="0" style="position:absolute;overflow:hidden">
<defs>

<!--
  BAROQUE FILIGREE — horizontal tile 400×32
  Traced from reference: symmetrical S-scroll + acanthus + central medallion
  All 90° corners replaced with r=6 arcs. Colour: #c8a96e (warm gold).
-->
<symbol id="fig-tile-h" viewBox="0 0 400 32" preserveAspectRatio="none">
<g fill="none" stroke="#c8a96e" stroke-width="0.9" stroke-linecap="round" stroke-linejoin="round">

  <!-- ── LEFT CORNER FLOURISH ── -->
  <!-- outer C-scroll, top-left, opening rightward -->
  <path d="
    M 2,2
    Q 2,8  8,10
    Q 18,14 22,10
    Q 26,6  22,3
    Q 18,1  14,4
    Q 11,6  13,9
    Q 15,11 18,9
  "/>
  <!-- tendril from left corner curving down -->
  <path d="
    M 2,2
    Q 2,16  8,20
    Q 14,24 20,20
    Q 24,17 22,13
  "/>
  <!-- small leaf off corner scroll -->
  <path d="M 6,14 Q 2,18 4,22 Q 6,25 10,22 Q 8,18 6,14 Z"/>
  <!-- inner S-scroll left -->
  <path d="
    M 22,16
    Q 28,12 34,16
    Q 40,20 36,24
    Q 32,27 28,24
    Q 25,22 27,19
    Q 29,17 32,19
  "/>
  <!-- connecting stem left→centre -->
  <path d="
    M 34,16
    Q 44,14 54,16
    Q 64,18 70,16
    Q 82,12 88,16
    Q 94,20 90,24
    Q 86,27 82,24
  "/>
  <!-- leaf cluster left-mid -->
  <path d="M 52,12 Q 56,6  62,10 Q 58,16 52,12 Z"/>
  <path d="M 58,10 Q 64,4  70,9  Q 66,15 58,10 Z"/>
  <path d="M 54,18 Q 50,24 56,26 Q 60,22 54,18 Z"/>
  <!-- small bud -->
  <circle cx="64" cy="8" r="1.4" fill="#c8a96e" stroke="none"/>
  <!-- S-scroll second wave left -->
  <path d="
    M 90,16
    Q 100,10 110,14
    Q 116,17 114,22
    Q 112,26 106,24
    Q 102,22 104,18
    Q 106,15 110,17
  "/>
  <!-- tendril spray left of centre -->
  <path d="M 112,12 Q 118,5  126,9  Q 122,17 112,12 Z"/>
  <path d="M 116,18 Q 124,24 120,28 Q 115,26 116,18 Z"/>
  <path d="
    M 114,16
    Q 124,14 132,16
    Q 138,18 140,16
  "/>

  <!-- ── CENTRE MEDALLION ── -->
  <!-- vertical spine -->
  <line x1="200" y1="4" x2="200" y2="28"/>
  <!-- top diamond -->
  <path d="M 200,4 L 196,9 L 200,14 L 204,9 Z"/>
  <!-- bottom diamond -->
  <path d="M 200,18 L 196,23 L 200,28 L 204,23 Z"/>
  <!-- left wing of medallion -->
  <path d="
    M 196,16
    Q 188,12 180,14
    Q 174,16 174,20
    Q 174,24 180,24
    Q 185,24 186,20
    Q 187,17 184,16
  "/>
  <!-- right wing of medallion -->
  <path d="
    M 204,16
    Q 212,12 220,14
    Q 226,16 226,20
    Q 226,24 220,24
    Q 215,24 214,20
    Q 213,17 216,16
  "/>
  <!-- medallion outer petals left -->
  <path d="M 178,12 Q 172,6  166,10 Q 168,17 178,12 Z"/>
  <path d="M 176,20 Q 168,26 170,30 Q 176,28 176,20 Z"/>
  <!-- medallion outer petals right -->
  <path d="M 222,12 Q 228,6  234,10 Q 232,17 222,12 Z"/>
  <path d="M 224,20 Q 232,26 230,30 Q 224,28 224,20 Z"/>
  <!-- centre dot -->
  <circle cx="200" cy="16" r="2" fill="#c8a96e" stroke="none"/>
  <!-- accent dots -->
  <circle cx="190" cy="16" r="1.2" fill="#c8a96e" stroke="none"/>
  <circle cx="210" cy="16" r="1.2" fill="#c8a96e" stroke="none"/>

  <!-- ── RIGHT SIDE — mirror of left ── -->
  <path d="
    M 260,16
    Q 266,18 268,16
    Q 276,14 286,16
  "/>
  <!-- tendril spray right of centre -->
  <path d="M 288,12 Q 282,5  274,9  Q 278,17 288,12 Z"/>
  <path d="M 284,18 Q 276,24 280,28 Q 285,26 284,18 Z"/>
  <!-- S-scroll second wave right -->
  <path d="
    M 290,16
    Q 298,10 308,14
    Q 316,17 314,22
    Q 312,26 306,24
    Q 302,22 304,18
    Q 306,15 310,17
  "/>
  <!-- leaf cluster right-mid -->
  <path d="M 348,12 Q 344,6  338,10 Q 342,16 348,12 Z"/>
  <path d="M 342,10 Q 336,4  330,9  Q 334,15 342,10 Z"/>
  <path d="M 346,18 Q 350,24 344,26 Q 340,22 346,18 Z"/>
  <circle cx="336" cy="8" r="1.4" fill="#c8a96e" stroke="none"/>
  <!-- connecting stem right -->
  <path d="
    M 310,16
    Q 316,12 322,16
    Q 328,20 324,24
    Q 320,27 316,24
    Q 313,22 315,19
    Q 317,17 320,19
  "/>
  <path d="
    M 322,16
    Q 332,14 342,16
    Q 348,18 356,16
    Q 362,12 366,16
    Q 370,20 366,24
    Q 362,27 358,24
  "/>
  <!-- inner S-scroll right -->
  <path d="
    M 366,16
    Q 372,12 378,16
    Q 384,20 380,24
    Q 376,27 372,24
    Q 369,22 371,19
    Q 373,17 376,19
  "/>
  <!-- small leaf right -->
  <path d="M 390,14 Q 398,18 394,22 Q 390,25 390,18 Z"/>
  <!-- right corner: tendril curving up -->
  <path d="
    M 398,2
    Q 398,16 392,20
    Q 386,24 380,20
    Q 376,17 378,13
  "/>
  <!-- outer C-scroll right -->
  <path d="
    M 398,2
    Q 398,8  392,10
    Q 382,14 378,10
    Q 374,6  378,3
    Q 382,1  386,4
    Q 389,6  387,9
    Q 385,11 382,9
  "/>
  <!-- corner leaf right -->
  <path d="M 394,14 Q 398,18 396,22 Q 394,25 390,22 Q 392,18 394,14 Z"/>

</g>
</symbol>

<!--
  BAROQUE FILIGREE — vertical tile 32×400
  Same motifs rotated 90°. All sharp corners radiused.
-->
<symbol id="fig-tile-v" viewBox="0 0 32 400" preserveAspectRatio="none">
<g fill="none" stroke="#c8a96e" stroke-width="0.9" stroke-linecap="round" stroke-linejoin="round">

  <!-- TOP CORNER FLOURISH -->
  <path d="M 16,2 Q 8,2 6,8 Q 4,18 8,22 Q 12,26 16,22 Q 19,18 16,14 Q 13,11 10,13 Q 8,15 10,18"/>
  <path d="M 16,2 Q 22,2 24,8 Q 26,18 22,22 Q 18,26 16,22"/>
  <path d="M 12,6 Q 6,2 2,6 Q 4,12 12,6 Z"/>
  <path d="M 20,6 Q 26,2 30,6 Q 28,12 20,6 Z"/>
  <circle cx="16" cy="14" r="1.4" fill="#c8a96e" stroke="none"/>

  <!-- UPPER S-SCROLL -->
  <path d="M 16,28 Q 10,34 12,42 Q 14,50 20,48 Q 25,46 24,40 Q 23,35 18,36 Q 14,37 15,42"/>
  <path d="M 8,36 Q 2,42 6,48 Q 10,52 14,48"/>
  <path d="M 24,36 Q 30,42 26,48 Q 22,52 18,48"/>
  <!-- leaf spray upper -->
  <path d="M 8,52 Q 2,56 4,62 Q 8,64 10,58 Q 9,55 8,52 Z"/>
  <path d="M 24,52 Q 30,56 28,62 Q 24,64 22,58 Q 23,55 24,52 Z"/>
  <path d="M 14,58 Q 12,66 16,68 Q 20,66 18,58 Q 17,58 14,58 Z"/>
  <circle cx="16" cy="50" r="1.2" fill="#c8a96e" stroke="none"/>

  <!-- UPPER STEM -->
  <line x1="16" y1="68" x2="16" y2="88"/>
  <path d="M 10,76 Q 4,78 4,84 Q 4,90 10,90 Q 14,90 15,86 Q 16,82 12,80"/>
  <path d="M 22,76 Q 28,78 28,84 Q 28,90 22,90 Q 18,90 17,86 Q 16,82 20,80"/>

  <!-- CENTRE MEDALLION -->
  <!-- horizontal spine -->
  <line x1="4" y1="200" x2="28" y2="200"/>
  <!-- left diamond -->
  <path d="M 4,200 L 9,196 L 14,200 L 9,204 Z"/>
  <!-- right diamond -->
  <path d="M 18,200 L 23,196 L 28,200 L 23,204 Z"/>
  <!-- top wing -->
  <path d="M 16,196 Q 12,188 14,180 Q 16,174 20,174 Q 24,174 24,180 Q 24,185 20,186 Q 17,187 16,184"/>
  <!-- bottom wing -->
  <path d="M 16,204 Q 12,212 14,220 Q 16,226 20,226 Q 24,226 24,220 Q 24,215 20,214 Q 17,213 16,216"/>
  <!-- medallion petals -->
  <path d="M 12,178 Q 6,172 10,166 Q 17,168 12,178 Z"/>
  <path d="M 20,178 Q 26,172 22,166 Q 15,168 20,178 Z"/>
  <path d="M 12,222 Q 6,228 10,234 Q 17,232 12,222 Z"/>
  <path d="M 20,222 Q 26,228 22,234 Q 15,232 20,222 Z"/>
  <circle cx="16" cy="200" r="2"   fill="#c8a96e" stroke="none"/>
  <circle cx="16" cy="190" r="1.2" fill="#c8a96e" stroke="none"/>
  <circle cx="16" cy="210" r="1.2" fill="#c8a96e" stroke="none"/>

  <!-- LOWER STEM -->
  <line x1="16" y1="240" x2="16" y2="260"/>
  <path d="M 10,248 Q 4,250 4,256 Q 4,262 10,262 Q 14,262 15,258 Q 16,254 12,252"/>
  <path d="M 22,248 Q 28,250 28,256 Q 28,262 22,262 Q 18,262 17,258 Q 16,254 20,252"/>

  <!-- LOWER S-SCROLL -->
  <path d="M 8,300 Q 2,306 6,312 Q 10,316 14,312"/>
  <path d="M 24,300 Q 30,306 26,312 Q 22,316 18,312"/>
  <path d="M 8,316 Q 2,320 4,326 Q 8,328 10,322 Q 9,319 8,316 Z"/>
  <path d="M 24,316 Q 30,320 28,326 Q 24,328 22,322 Q 23,319 24,316 Z"/>
  <path d="M 14,320 Q 12,328 16,330 Q 20,328 18,320 Q 17,320 14,320 Z"/>
  <circle cx="16" cy="308" r="1.2" fill="#c8a96e" stroke="none"/>
  <path d="M 16,330 Q 10,336 12,344 Q 14,352 20,350 Q 25,348 24,342 Q 23,337 18,338 Q 14,339 15,344"/>

  <!-- BOTTOM CORNER FLOURISH -->
  <path d="M 16,372 Q 8,372 6,378 Q 4,388 8,392 Q 12,396 16,392"/>
  <path d="M 16,372 Q 22,372 24,378 Q 26,388 22,392 Q 18,396 16,392"/>
  <path d="M 12,376 Q 6,370 2,374 Q 4,380 12,376 Z"/>
  <path d="M 20,376 Q 26,370 30,374 Q 28,380 20,376 Z"/>
  <circle cx="16" cy="384" r="1.4" fill="#c8a96e" stroke="none"/>

</g>
</symbol>

<!--
  CORNER MEDALLION — 32×32, used at intersection of h+v dividers
-->
<symbol id="fig-corner" viewBox="0 0 32 32" preserveAspectRatio="xMidYMid meet">
<g fill="none" stroke="#c8a96e" stroke-width="0.9" stroke-linecap="round" stroke-linejoin="round">
  <!-- Radiused corner arc replacing the 90° joint -->
  <path d="M 16,2 Q 16,16 30,16" stroke-width="1.1"/>
  <!-- Surrounding scrollwork -->
  <path d="M 16,2 Q 8,4 6,10 Q 5,16 10,18 Q 14,19 15,15 Q 15,11 11,11"/>
  <path d="M 30,16 Q 28,8 22,6 Q 16,5 14,10 Q 13,14 17,15 Q 21,16 21,12"/>
  <!-- petals -->
  <path d="M 6,6 Q 2,2 2,8 Q 6,9 6,6 Z"/>
  <path d="M 26,6 Q 30,2 30,8 Q 26,9 26,6 Z"/>
  <circle cx="16" cy="16" r="1.8" fill="#c8a96e" stroke="none"/>
</g>
</symbol>

<!--
  PATTERNS — reference the symbols as tiled patterns
-->
<pattern id="fig-h-pat" x="0" y="0" width="400" height="32" patternUnits="userSpaceOnUse">
  <use href="#fig-tile-h" width="400" height="32"/>
</pattern>
<pattern id="fig-v-pat" x="0" y="0" width="32" height="400" patternUnits="userSpaceOnUse">
  <use href="#fig-tile-v" width="32" height="400"/>
</pattern>

</defs>
</svg>

<div class="max-w-[1300px] mx-auto flex relative">

  <!-- LEFT SIDEBAR -->
  <aside class="w-16 xl:w-64 h-screen sticky top-0 flex-shrink-0 flex flex-col
                px-2 xl:px-4 py-3 relative">
    <!-- Vertical filigree border-r -->
    <svg class="filigree-v" viewBox="0 0 32 800" preserveAspectRatio="none">
      <rect width="32" height="800" fill="url(#fig-v-pat)"/>
    </svg>

    <div class="mb-4 px-2">
      <span class="text-2xl font-black tracking-tight">flex<span class="text-blue-400">.</span></span>
    </div>
    <nav class="flex-1 space-y-1">
      <button onclick="nav('feed')" id="nav-feed"
        class="sidebar-item active w-full flex items-center gap-3 px-3 py-3 rounded-xl text-left">
        <i class="fa-solid fa-house w-5 text-center text-lg"></i>
        <span class="hidden xl:inline">Home</span>
      </button>
      <button onclick="nav('explore')" id="nav-explore"
        class="sidebar-item w-full flex items-center gap-3 px-3 py-3 rounded-xl text-left">
        <i class="fa-solid fa-magnifying-glass w-5 text-center text-lg"></i>
        <span class="hidden xl:inline">Explore</span>
      </button>
      <button onclick="nav('messages')" id="nav-messages"
        class="sidebar-item w-full flex items-center gap-3 px-3 py-3 rounded-xl text-left relative">
        <i class="fa-solid fa-lock w-5 text-center text-lg"></i>
        <span class="hidden xl:inline">Messages</span>
        <span id="dm-badge"
          class="hidden absolute top-1.5 left-5 w-4 h-4 bg-red-500 rounded-full
                 text-xs flex items-center justify-center font-bold"></span>
      </button>
      <button onclick="nav('profile')" id="nav-profile"
        class="sidebar-item w-full flex items-center gap-3 px-3 py-3 rounded-xl text-left">
        <i class="fa-solid fa-user w-5 text-center text-lg"></i>
        <span class="hidden xl:inline">Profile</span>
      </button>
    </nav>
    <button onclick="openCompose()"
      class="btn-blue mt-4 w-10 xl:w-full py-3 rounded-2xl font-bold text-white
             flex items-center justify-center gap-2">
      <i class="fa-solid fa-plus xl:hidden"></i>
      <span class="hidden xl:inline">Post</span>
    </button>

    <!-- Auto-lock speed control -->
    <div class="relative mt-2 hidden xl:block">
      <div id="ss-speed-panel">
        <p class="text-xs text-gray-500 mb-2 font-semibold tracking-wide uppercase">
          <i class="fa-solid fa-shield-halved text-green-500 mr-1"></i>
          Auto-lock timeout
        </p>
        <div class="flex gap-1 flex-wrap">
          <button class="speed-btn active" data-secs="0"   onclick="setSSTiming(0)">Off</button>
          <button class="speed-btn"        data-secs="30"  onclick="setSSTiming(30)">30s</button>
          <button class="speed-btn"        data-secs="60"  onclick="setSSTiming(60)">1m</button>
          <button class="speed-btn"        data-secs="120" onclick="setSSTiming(120)">2m</button>
          <button class="speed-btn"        data-secs="300" onclick="setSSTiming(300)">5m</button>
          <button class="speed-btn"        data-secs="600" onclick="setSSTiming(600)">10m</button>
        </div>
        <p class="text-xs text-gray-600 mt-2">Locks and obscures your session after idle.</p>
      </div>
      <button id="ss-speed-toggle" onclick="toggleSSPanel()">
        <i class="fa-solid fa-shield-halved text-green-500"></i>
        <span>Auto-lock</span>
        <i id="ss-panel-caret" class="fa-solid fa-chevron-up ml-auto text-gray-600 text-xs"></i>
      </button>
    </div>

    <div id="sidebar-user" class="mt-4 pt-4 relative">
      <!-- filigree above user widget -->
      <svg class="filigree-h mb-1" viewBox="0 0 400 32" preserveAspectRatio="none">
        <rect width="400" height="32" fill="url(#fig-h-pat)"/>
      </svg>
    </div>
  </aside>

  <!-- MAIN -->
  <main class="flex-1 min-w-0 max-w-[620px] relative min-h-screen">
    <!-- Vertical filigree border-r -->
    <svg class="filigree-v" viewBox="0 0 32 800" preserveAspectRatio="none">
      <rect width="32" height="800" fill="url(#fig-v-pat)"/>
    </svg>

    <header class="sticky top-0 bg-black/85 backdrop-blur z-40 px-4 py-3
                   flex items-center justify-between">
      <h1 id="page-title" class="text-xl font-bold">Home</h1>
      <div id="ws-dot" class="w-2 h-2 rounded-full bg-gray-600" title="Connecting…"></div>
    </header>
    <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>

    <!-- FEED -->
    <div id="page-feed">
      <div id="inline-composer" class="hidden px-4 py-4"></div>
      <svg id="composer-filigree" class="filigree-h hidden" viewBox="0 0 400 32" preserveAspectRatio="none">
        <rect width="400" height="32" fill="url(#fig-h-pat)"/>
      </svg>
      <div id="feed-list"></div>
    </div>

    <!-- EXPLORE -->
    <div id="page-explore" class="hidden px-4 py-4">
      <div class="relative mb-4">
        <input id="search-input" type="text" placeholder="Search posts and people…"
          class="w-full bg-gray-900 border border-gray-700 focus:border-blue-500
                 rounded-2xl py-3 pl-11 pr-4 outline-none text-base">
        <i class="fa-solid fa-magnifying-glass absolute left-4 top-3.5 text-gray-500"></i>
      </div>
      <div id="search-results"></div>
    </div>

    <!-- PROFILE -->
    <div id="page-profile" class="hidden">
      <div id="profile-content"></div>
    </div>

    <!-- THREAD -->
    <div id="page-thread" class="hidden">
      <button onclick="nav('feed')"
        class="flex items-center gap-2 px-4 py-4 text-gray-400 hover:text-white text-sm">
        <i class="fa-solid fa-arrow-left"></i> Back
      </button>
      <div id="thread-content"></div>
    </div>

    <!-- MESSAGES -->
    <div id="page-messages" class="hidden">
      <div id="inbox-view">
        <div class="px-4 py-4 flex items-center justify-between">
          <h2 class="font-bold text-lg">Encrypted Messages</h2>
          <div class="text-xs text-green-400 flex items-center gap-1.5">
            <div class="w-2 h-2 rounded-full bg-green-500"></div>
            CTW-BSC ready
          </div>
        </div>
        <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
          <rect width="400" height="32" fill="url(#fig-h-pat)"/>
        </svg>
        <div class="px-4 py-3">
          <button onclick="openNewDM()"
            class="btn-blue w-full py-2.5 rounded-2xl font-bold text-sm
                   flex items-center justify-center gap-2">
            <i class="fa-solid fa-lock"></i> New Encrypted Message
          </button>
        </div>
        <div id="inbox-list"></div>
      </div>

      <div id="conversation-view" class="hidden flex-col">
        <div class="sticky top-0 bg-black px-4 py-3 flex items-center gap-3 z-10">
          <button onclick="showInbox()" class="text-gray-400 hover:text-white">
            <i class="fa-solid fa-arrow-left"></i>
          </button>
          <div>
            <div id="conv-title" class="font-bold"></div>
            <div class="text-xs text-gray-500 flex items-center gap-1">
              <i class="fa-solid fa-lock text-green-400"></i>
              CTW-BSC End-to-End Encrypted
            </div>
          </div>
        </div>
        <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
          <rect width="400" height="32" fill="url(#fig-h-pat)"/>
        </svg>
        <div class="bg-gray-950 cipher-glow">
          <div id="conv-explainer" class="px-4 py-4">
            <div class="flex gap-3">
              <div class="flex-shrink-0 w-8 h-8 rounded-xl bg-green-500/10 border border-green-500/30
                          flex items-center justify-center">
                <i class="fa-solid fa-lock text-green-400 text-sm"></i>
              </div>
              <div>
                <p class="text-sm font-semibold text-white mb-1">CTW-BSC End-to-End Encrypted</p>
                <p class="text-xs text-gray-400 leading-relaxed">
                  Enter the shared passphrase you received from your contact
                  <strong class="text-gray-200">outside this platform</strong>.
                </p>
              </div>
            </div>
          </div>
          <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
            <rect width="400" height="32" fill="url(#fig-h-pat)"/>
          </svg>
          <div class="px-4 py-3 flex items-center gap-2">
            <i class="fa-solid fa-key text-yellow-400 text-sm flex-shrink-0"></i>
            <input id="conv-passphrase" type="password"
              placeholder="Enter shared passphrase to unlock…"
              class="flex-1 bg-transparent outline-none text-sm placeholder-gray-600"
              onkeydown="if(event.key==='Enter') loadConversation(currentConvUser)">
            <button onclick="loadConversation(currentConvUser)"
              class="text-blue-400 hover:text-blue-300 text-xs font-bold
                     px-3 py-1.5 border border-gray-700 rounded-xl flex-shrink-0
                     hover:border-blue-500 transition-colors">
              Decrypt
            </button>
          </div>
          <div id="conv-hint" class="px-4 pb-3 text-xs text-gray-600 hidden">
            <i class="fa-solid fa-circle-info text-gray-700 mr-1"></i>
            <span id="conv-hint-text"></span>
          </div>
        </div>
        <div id="conv-messages" class="overflow-y-auto px-4 py-4 space-y-4"
          style="min-height:300px;max-height:calc(100vh - 360px)"></div>
        <div class="px-4 py-4 bg-black sticky bottom-0">
          <svg class="filigree-h mb-3" viewBox="0 0 400 32" preserveAspectRatio="none">
            <rect width="400" height="32" fill="url(#fig-h-pat)"/>
          </svg>
          <div class="flex gap-3 items-end">
            <textarea id="dm-compose" rows="2"
              placeholder="Write encrypted message…"
              class="flex-1 bg-gray-900 border border-gray-700 rounded-2xl
                     px-4 py-3 outline-none resize-none text-sm
                     focus:border-blue-500 placeholder-gray-600"
              onkeydown="if(event.key==='Enter'&&!event.shiftKey){event.preventDefault();sendDM();}"></textarea>
            <button onclick="sendDM()" id="dm-send-btn"
              class="btn-blue px-4 py-3 rounded-2xl font-bold text-sm flex-shrink-0">
              <i class="fa-solid fa-lock"></i>
            </button>
          </div>
          <div class="text-xs text-gray-600 mt-2 flex items-center gap-1">
            <i class="fa-solid fa-shield-halved text-green-500"></i>
            Encrypted with CTW-BSC on the server before storage
          </div>
        </div>
      </div>
    </div>
  </main>

  <!-- RIGHT SIDEBAR -->
  <aside class="w-80 hidden lg:block flex-shrink-0 px-4 py-4
                sticky top-0 h-screen overflow-y-auto">
    <div class="relative mb-6">
      <input id="rs-search" type="text" placeholder="Search"
        class="w-full bg-gray-900 border border-gray-700 focus:border-blue-500
               rounded-2xl py-3 pl-11 pr-4 outline-none">
      <i class="fa-solid fa-magnifying-glass absolute left-4 top-3.5 text-gray-500"></i>
    </div>
    <div class="bg-gray-900 rounded-2xl overflow-hidden mb-6">
      <h2 class="font-bold text-lg px-4 pt-4 pb-2">In the know</h2>
      <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
        <rect width="400" height="32" fill="url(#fig-h-pat)"/>
      </svg>
      <div id="trending-list"></div>
    </div>
    <div class="bg-gray-900 rounded-2xl overflow-hidden">
      <h2 class="font-bold text-lg px-4 pt-4 pb-2">Up and coming</h2>
      <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
        <rect width="400" height="32" fill="url(#fig-h-pat)"/>
      </svg>
      <div id="follow-list"></div>
    </div>
  </aside>
</div>

<!-- COMPOSE MODAL -->
<div id="compose-modal"
  class="hidden fixed inset-0 bg-black/70 z-50 flex items-start justify-center pt-16 px-4">
  <div class="bg-black border border-gray-700 rounded-2xl w-full max-w-lg">
    <div class="flex items-center justify-between px-4 py-3">
      <button onclick="closeCompose()"
        class="text-gray-400 hover:text-white w-9 h-9 rounded-full
               hover:bg-gray-800 flex items-center justify-center">
        <i class="fa-solid fa-xmark"></i>
      </button>
      <button onclick="submitPost()" id="modal-post-btn"
        class="btn-blue px-5 py-1.5 rounded-2xl font-bold text-sm">Post</button>
    </div>
    <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <div class="flex gap-3 px-4 py-4">
      <div id="modal-avatar-initials"
        class="w-10 h-10 rounded-full flex-shrink-0 flex items-center justify-center font-bold text-lg"></div>
      <div class="flex-1">
        <textarea id="modal-text" rows="5"
          class="w-full bg-transparent outline-none text-lg resize-none placeholder-gray-600"
          placeholder="What's happening?!"
          oninput="updateCharCount()"></textarea>
        <div id="media-preview" class="flex flex-wrap gap-2 mt-2"></div>
      </div>
    </div>
    <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <div class="flex items-center justify-between px-4 py-3">
      <label class="cursor-pointer text-blue-400 hover:text-blue-300">
        <i class="fa-regular fa-image text-xl"></i>
        <input type="file" id="media-input" class="hidden"
               accept="image/*,video/mp4" multiple onchange="previewMedia()">
      </label>
      <div class="flex items-center gap-3">
        <svg class="w-8 h-8 -rotate-90" viewBox="0 0 32 32">
          <circle cx="16" cy="16" r="12" fill="none" stroke="#333" stroke-width="3"/>
          <circle id="char-ring" cx="16" cy="16" r="12" fill="none"
            stroke="var(--blue)" stroke-width="3" stroke-linecap="round"
            stroke-dasharray="75.4" stroke-dashoffset="75.4" class="char-ring"/>
        </svg>
        <span id="char-count" class="text-sm text-gray-500">500</span>
      </div>
    </div>
  </div>
</div>

<!-- AUTH MODAL -->
<div id="auth-modal"
  class="hidden fixed inset-0 bg-black/80 z-50 flex items-center justify-center px-4">
  <div class="bg-black border border-gray-700 rounded-2xl w-full max-w-sm p-6">
    <div class="flex mb-4 gap-1">
      <button onclick="setAuthTab('login')" id="tab-login"
        class="flex-1 pb-3 font-bold border-b-2 border-blue-500 text-blue-400">Sign in</button>
      <button onclick="setAuthTab('register')" id="tab-register"
        class="flex-1 pb-3 font-bold border-b-2 border-transparent text-gray-500">Sign up</button>
    </div>
    <svg class="filigree-h mb-4" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <div id="auth-login">
      <input id="login-user" type="text" placeholder="Username"
        class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-3
               outline-none focus:border-blue-500">
      <input id="login-pass" type="password" placeholder="Password"
        class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-4
               outline-none focus:border-blue-500"
        onkeydown="if(event.key==='Enter') doLogin()">
      <button onclick="doLogin()" class="btn-blue w-full py-3 rounded-2xl font-bold">Sign in</button>
    </div>
    <div id="auth-register" class="hidden">
      <input id="reg-user" type="text" placeholder="Username (2+ chars)"
        class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-3
               outline-none focus:border-blue-500">
      <input id="reg-display" type="text" placeholder="Display name"
        class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-3
               outline-none focus:border-blue-500">
      <input id="reg-pass" type="password" placeholder="Password (6+ chars)"
        class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-4
               outline-none focus:border-blue-500"
        onkeydown="if(event.key==='Enter') doRegister()">
      <button onclick="doRegister()" class="btn-blue w-full py-3 rounded-2xl font-bold">Create account</button>
    </div>
    <p id="auth-error" class="text-red-400 text-sm mt-3 hidden"></p>
    <button onclick="closeAuth()" class="w-full mt-3 text-gray-500 text-sm hover:text-white">Continue as guest</button>
  </div>
</div>

<!-- NEW DM MODAL -->
<div id="new-dm-modal"
  class="hidden fixed inset-0 bg-black/80 z-50 flex items-center justify-center px-4">
  <div class="bg-black border border-gray-700 rounded-2xl w-full max-w-sm p-6">
    <div class="flex items-center justify-between mb-4">
      <h3 class="font-bold text-lg flex items-center gap-2">
        <i class="fa-solid fa-lock text-green-400"></i> New Encrypted Message
      </h3>
      <button onclick="closeNewDM()" class="text-gray-500 hover:text-white">
        <i class="fa-solid fa-xmark"></i>
      </button>
    </div>
    <svg class="filigree-h mb-4" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <label class="text-xs text-gray-400 mb-1 block">Recipient username</label>
    <input id="new-dm-recipient" type="text" placeholder="@username"
      class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-4
             outline-none focus:border-blue-500 text-sm">
    <label class="text-xs text-gray-400 mb-1 block">Conversation passphrase</label>
    <input id="new-dm-passphrase" type="password"
      placeholder="Min 12 chars, 2+ character classes"
      class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-3
             outline-none focus:border-blue-500 text-sm"
      onkeydown="if(event.key==='Enter') startNewDM()">
    <label class="text-xs text-gray-400 mb-1 block">Passphrase hint (optional)</label>
    <input id="new-dm-hint" type="text" placeholder="Hint visible to both parties"
      class="w-full bg-gray-900 border border-gray-700 rounded-xl px-4 py-3 mb-4
             outline-none focus:border-gray-600 text-sm text-gray-400">
    <button onclick="startNewDM()" class="btn-blue w-full py-3 rounded-2xl font-bold">Open Conversation</button>
    <p id="new-dm-error" class="text-red-400 text-xs mt-3 hidden"></p>
  </div>
</div>

<!-- LOCK OVERLAY — no password, just blocks UI -->
<div id="lock-overlay">
  <div class="lock-box">
    <!-- top filigree in lock box -->
    <svg style="width:260px;height:32px" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <i class="fa-solid fa-shield-halved text-4xl" style="color:var(--gold)"></i>
    <div class="text-center">
      <p class="font-bold text-lg text-white">Session Locked</p>
      <p class="lock-status">CTW · Auto-lock active</p>
    </div>
    <p class="text-xs text-gray-600 text-center leading-relaxed max-w-xs">
      Your session is protected. Click the button below or interact with the screen to resume.
    </p>
    <!-- bottom filigree -->
    <svg style="width:260px;height:32px" viewBox="0 0 400 32" preserveAspectRatio="none">
      <rect width="400" height="32" fill="url(#fig-h-pat)"/>
    </svg>
    <button onclick="unlockSession()"
      class="btn-blue w-full py-2.5 rounded-2xl font-bold flex items-center justify-center gap-2">
      <i class="fa-solid fa-lock-open"></i> Resume Session
    </button>
    <button onclick="lockLogout()" class="text-xs mt-1" style="color:#444">
      Sign out instead
    </button>
  </div>
</div>

<!-- TOAST -->
<div id="toast"
  class="hidden fixed bottom-6 left-1/2 -translate-x-1/2 px-6 py-3 rounded-2xl
         text-sm font-medium shadow-xl z-[100] text-white"></div>

<!-- SCREENSAVER CANVAS -->
<canvas id="screensaver-canvas"
  class="hidden fixed inset-0 z-[200] cursor-none"
  style="background:#000;width:100vw;height:100vh;"></canvas>

<script>
// ═══════════════════════════════════════════════════════
// CONFIG
// ═══════════════════════════════════════════════════════
const API    = (location.hostname==='localhost'||location.hostname==='127.0.0.1')
               ? 'http://localhost:8000' : window.location.origin;
const WS_URL = API.replace(/^http/,'ws') + '/ws';

// ═══════════════════════════════════════════════════════
// STATE
// ═══════════════════════════════════════════════════════
let token             = localStorage.getItem('token') || null;
let me                = JSON.parse(localStorage.getItem('me') || 'null');
let feedData          = [];
let feedOffset        = 0;
let loadingMore       = false;
let currentPage       = 'feed';
let composeParentId   = null;
let mediaFiles        = [];
let ws                = null;
let wsRetries         = 0;
let currentConvUser   = null;
let convMessages      = [];
let dmPassphraseCache = {};
let searchTimer       = null;

// ═══════════════════════════════════════════════════════
// API
// ═══════════════════════════════════════════════════════
async function api(method, path, body=null) {
  const opts = { method, headers:{'Content-Type':'application/json'} };
  const currentToken = token || localStorage.getItem('token');
  if (currentToken) opts.headers['Authorization'] = `Bearer ${currentToken}`;
  if (body) opts.body = JSON.stringify(body);
  const r = await fetch(API + path, opts);
  if (!r.ok) {
    const err = await r.json().catch(()=>({detail:r.statusText}));
    throw new Error(err.detail || r.statusText);
  }
  return r.json();
}
async function uploadMedia(postId, file) {
  const fd = new FormData();
  fd.append('file', file);
  const r = await fetch(`${API}/api/posts/${postId}/media`, {
    method:'POST', headers:{'Authorization':`Bearer ${token}`}, body:fd
  });
  if (!r.ok) throw new Error('Media upload failed');
  return r.json();
}

// ═══════════════════════════════════════════════════════
// WEBSOCKET
// ═══════════════════════════════════════════════════════
function connectWS() {
  if (ws) { try { ws.close(); } catch(e) {} }
  const t = token ? token : '';
  ws = new WebSocket(`${WS_URL}?token=${encodeURIComponent(t)}`);
  ws.onopen = () => {
    wsRetries = 0; setWsDot('bg-green-500','Live');
    if (token) ws.send(JSON.stringify({type:'auth',token}));
    setInterval(()=>ws.readyState===1&&ws.send('ping'),30000);
  };
  ws.onmessage = ({data}) => {
    try {
      const {event,data:d} = JSON.parse(data);
      if (event==='new_post')      handleNewPost(d);
      if (event==='like_update')   handleLikeUpdate(d);
      if (event==='repost_update') handleRepostUpdate(d);
      if (event==='post_update')   handlePostUpdate(d);
      if (event==='new_dm')        handleNewDM(d);
      if (event==='dm_sent')       handleDMSent(d);
    } catch(e) {}
  };
  ws.onclose = () => {
    setWsDot('bg-red-500','Reconnecting…');
    setTimeout(connectWS, Math.min(1000*2**wsRetries++,30000));
  };
}
function setWsDot(cls,title) {
  const el=document.getElementById('ws-dot');
  el.className=`w-2 h-2 rounded-full ${cls}`; el.title=title;
}
function handleNewPost(post) {
  if (post.parent_id) return;
  if (feedData.find(p=>p.id===post.id)) return;
  feedData.unshift(post);
  document.getElementById('feed-list').prepend(buildPostCard(post));
  if (post.author!==me?.username) toast('New post ↑');
}
function handlePostUpdate(post) {
  const idx=feedData.findIndex(p=>p.id===post.id);
  if (idx!==-1) feedData[idx]=post;
  const el=document.querySelector(`[data-post="${post.id}"]`);
  if (el) el.replaceWith(buildPostCard(post));
}
function handleLikeUpdate({post_id,like_count,liked,by}) {
  const p=feedData.find(p=>p.id===post_id);
  if (p) { p.like_count=like_count; if(by===me?.username) p.liked=liked; }
  const el=document.querySelector(`[data-post="${post_id}"] .like-count`);
  if (el) el.textContent=like_count;
  if (by===me?.username) {
    const btn=document.querySelector(`[data-post="${post_id}"] .like-btn`);
    if (btn) btn.classList.toggle('text-red-500',liked);
  }
}
function handleRepostUpdate({post_id,repost_count}) {
  const p=feedData.find(p=>p.id===post_id);
  if (p) p.repost_count=repost_count;
  const el=document.querySelector(`[data-post="${post_id}"] .repost-count`);
  if (el) el.textContent=repost_count;
}
function handleNewDM(msg) {
  const badge=document.getElementById('dm-badge');
  const n=parseInt(badge.textContent||'0')+1;
  badge.textContent=n>9?'9+':n; badge.classList.remove('hidden');
  if (currentPage==='messages'&&currentConvUser===msg.sender) {
    if (!convMessages.find(m=>m.id===msg.id)) {
      convMessages.push({...msg,decrypt_ok:true}); renderMessages();
    }
  } else toast(`🔒 Encrypted message from @${msg.sender}`);
}
function handleDMSent(msg) {
  if (currentConvUser===msg.recipient) {
    if (!convMessages.find(m=>m.id===msg.id)) {
      convMessages.push({...msg,decrypt_ok:true}); renderMessages();
    }
  }
}

// ═══════════════════════════════════════════════════════
// AUTH
// ═══════════════════════════════════════════════════════
function openAuth()  { document.getElementById('auth-modal').classList.remove('hidden'); }
function closeAuth() { document.getElementById('auth-modal').classList.add('hidden'); }
function setAuthTab(tab) {
  document.getElementById('auth-login').classList.toggle('hidden',tab!=='login');
  document.getElementById('auth-register').classList.toggle('hidden',tab!=='register');
  const on='flex-1 pb-3 font-bold border-b-2 border-blue-500 text-blue-400';
  const off='flex-1 pb-3 font-bold border-b-2 border-transparent text-gray-500';
  document.getElementById('tab-login').className=tab==='login'?on:off;
  document.getElementById('tab-register').className=tab==='register'?on:off;
  document.getElementById('auth-error').classList.add('hidden');
}
async function doLogin() {
  const username=document.getElementById('login-user').value.trim();
  const password=document.getElementById('login-pass').value;
  try {
    const res=await api('POST','/api/login',{username,password});
    saveSession(res.token,res.user);
    closeAuth(); renderSidebarUser(); renderInlineComposer();
    toast(`Welcome back, ${me.display_name}!`);
    loadFeed(true); connectWS();
  } catch(e) { showAuthError(e.message); }
}
async function doRegister() {
  const username=document.getElementById('reg-user').value.trim();
  const display_name=document.getElementById('reg-display').value.trim();
  const password=document.getElementById('reg-pass').value;
  try {
    const res=await api('POST','/api/register',{username,password,display_name});
    saveSession(res.token,res.user);
    closeAuth(); renderSidebarUser(); renderInlineComposer();
    toast(`Welcome, ${me.display_name}!`);
    loadFeed(true); connectWS();
  } catch(e) { showAuthError(e.message); }
}
function saveSession(t,user) {
  token=t; me=user;
  localStorage.setItem('token',t);
  localStorage.setItem('me',JSON.stringify(user));
}
function logout() {
  token=null; me=null;
  localStorage.removeItem('token'); localStorage.removeItem('me');
  dmPassphraseCache={};
  renderSidebarUser(); renderInlineComposer();
  loadFeed(true); toast('Signed out.');
}
function showAuthError(msg) {
  const el=document.getElementById('auth-error');
  el.textContent=msg; el.classList.remove('hidden');
}

// ═══════════════════════════════════════════════════════
// NAVIGATION
// ═══════════════════════════════════════════════════════
function nav(page,arg) {
  ['feed','explore','profile','thread','messages'].forEach(p=>{
    document.getElementById(`page-${p}`).classList.toggle('hidden',p!==page);
  });
  ['feed','explore','profile','messages'].forEach(p=>{
    document.getElementById(`nav-${p}`)?.classList.toggle('active',p===page);
  });
  currentPage=page;
  const titles={feed:'Home',explore:'Explore',profile:'Profile',thread:'Thread',messages:'Encrypted Messages'};
  document.getElementById('page-title').textContent=titles[page]||page;
  if (page==='feed')    { loadFeed(); renderInlineComposer(); }
  if (page==='explore') { setTimeout(()=>document.getElementById('search-input').focus(),50); }
  if (page==='profile') { loadProfile(arg||me?.username); }
  if (page==='thread')  { loadThread(arg); }
  if (page==='messages') {
    if (!requireAuth()) return;
    loadInbox();
    if (arg) openConversation(arg);
  }
}

// ═══════════════════════════════════════════════════════
// FILIGREE DIVIDER helper
// ═══════════════════════════════════════════════════════
function makeFigH() {
  const svg = document.createElementNS('http://www.w3.org/2000/svg','svg');
  svg.setAttribute('class','filigree-h');
  svg.setAttribute('viewBox','0 0 400 32');
  svg.setAttribute('preserveAspectRatio','none');
  const rect = document.createElementNS('http://www.w3.org/2000/svg','rect');
  rect.setAttribute('width','400'); rect.setAttribute('height','32');
  rect.setAttribute('fill','url(#fig-h-pat)');
  svg.appendChild(rect);
  return svg;
}

// ═══════════════════════════════════════════════════════
// FEED
// ═══════════════════════════════════════════════════════
async function loadFeed(reset=false) {
  if (reset) { feedData=[]; feedOffset=0; document.getElementById('feed-list').innerHTML=''; }
  if (loadingMore) return;
  loadingMore=true;
  showSkeleton('feed-list',reset?5:3);
  try {
    const posts=await api('GET',`/api/feed?offset=${feedOffset}&limit=20`);
    removeSkeleton('feed-list');
    feedData.push(...posts);
    feedOffset+=posts.length;
    const list=document.getElementById('feed-list');
    posts.forEach(p=>list.appendChild(buildPostCard(p)));
    if (!posts.length)
      list.innerHTML+='<p class="text-center text-gray-600 py-12 text-sm">No more posts.</p>';
  } catch(e) {
    removeSkeleton('feed-list');
    showError('feed-list','Could not load feed — is the backend running?');
  }
  loadingMore=false;
}

// ═══════════════════════════════════════════════════════
// POST CARD
// ═══════════════════════════════════════════════════════
function buildPostCard(post) {
  const wrap=document.createElement('div');
  wrap.dataset.post=post.id;

  const div=document.createElement('div');
  div.className='post-card px-4 py-4 cursor-pointer';
  div.addEventListener('click',()=>nav('thread',post.id));

  const avatarHTML=post.avatar_url
    ?`<img src="${post.avatar_url}" class="avatar w-10 h-10 rounded-full flex-shrink-0">`
    :`<div class="w-10 h-10 rounded-full flex-shrink-0 flex items-center justify-center font-bold text-lg"
         style="background:${colorFor(post.author)}">${(post.display_name||'?')[0].toUpperCase()}</div>`;

  const mediaHTML=(post.media||[]).map(m=>
    m.mime&&m.mime.startsWith('video')
      ?`<video src="${m.url}" controls class="rounded-xl mt-3 max-h-80 w-full object-cover border border-gray-800"></video>`
      :`<img src="${m.url}" loading="lazy" class="rounded-xl mt-3 max-h-80 w-full object-cover border border-gray-800">`
  ).join('');

  div.innerHTML=`
    <div class="flex gap-3">
      ${avatarHTML}
      <div class="flex-1 min-w-0">
        <div class="flex items-center gap-2 flex-wrap">
          <span class="font-bold text-sm hover:underline cursor-pointer"
            onclick="event.stopPropagation();nav('profile','${post.author}')">
            ${esc(post.display_name)}</span>
          <span class="text-gray-500 text-sm">@${esc(post.author)}</span>
          <span class="text-gray-600 text-xs">· ${timeAgo(post.created_at)}</span>
        </div>
        ${post.parent_id?'<div class="text-xs text-gray-500 mb-1">Replying to thread</div>':''}
        <p class="text-sm leading-relaxed mt-1 whitespace-pre-wrap break-words">${linkify(esc(post.content))}</p>
        ${mediaHTML}
        <div class="flex items-center gap-6 mt-4 text-gray-500 text-sm">
          <button class="flex items-center gap-1.5 hover:text-blue-400 transition-colors"
            onclick="event.stopPropagation();replyTo('${post.id}')">
            <i class="fa-regular fa-comment"></i><span>${(post.children||[]).length}</span>
          </button>
          <button class="flex items-center gap-1.5 hover:text-green-400 transition-colors ${post.reposted?'text-green-400':''}"
            onclick="event.stopPropagation();toggleRepost('${post.id}')">
            <i class="fa-solid fa-retweet"></i><span class="repost-count">${post.repost_count}</span>
          </button>
          <button class="like-btn flex items-center gap-1.5 hover:text-red-400 transition-colors ${post.liked?'text-red-500':''}"
            onclick="event.stopPropagation();toggleLike('${post.id}')">
            <i class="${post.liked?'fa-solid':'fa-regular'} fa-heart"></i>
            <span class="like-count">${post.like_count}</span>
          </button>
          <button class="flex items-center gap-1.5 hover:text-blue-400 transition-colors"
            onclick="event.stopPropagation();sharePost('${post.id}')">
            <i class="fa-solid fa-arrow-up-from-bracket"></i>
          </button>
        </div>
      </div>
    </div>`;

  wrap.appendChild(div);
  wrap.appendChild(makeFigH());
  return wrap;
}

// ═══════════════════════════════════════════════════════
// INTERACTIONS
// ═══════════════════════════════════════════════════════
async function toggleLike(postId) {
  if (!requireAuth()) return;
  try { await api('POST',`/api/posts/${postId}/like`); }
  catch(e) { toast(e.message,true); }
}
async function toggleRepost(postId) {
  if (!requireAuth()) return;
  try { await api('POST',`/api/posts/${postId}/repost`); toast('Reposted!'); }
  catch(e) { toast(e.message,true); }
}
function replyTo(postId) {
  if (!requireAuth()) return;
  composeParentId=postId; openCompose();
  document.getElementById('modal-text').placeholder='Post your reply…';
}
function sharePost(postId) {
  navigator.clipboard.writeText(`${location.origin}/?thread=${postId}`)
    .then(()=>toast('Link copied!'));
}

// ═══════════════════════════════════════════════════════
// COMPOSE
// ═══════════════════════════════════════════════════════
function openCompose() {
  if (!requireAuth()) return;
  const initials=document.getElementById('modal-avatar-initials');
  initials.textContent=(me?.display_name||'?')[0].toUpperCase();
  initials.style.background=colorFor(me?.username||'');
  document.getElementById('modal-text').value='';
  document.getElementById('media-preview').innerHTML='';
  mediaFiles=[]; updateCharCount();
  document.getElementById('compose-modal').classList.remove('hidden');
  document.getElementById('modal-text').focus();
}
function closeCompose() {
  document.getElementById('compose-modal').classList.add('hidden');
  composeParentId=null;
  document.getElementById('modal-text').placeholder="What's happening?!";
}
function updateCharCount() {
  const text=document.getElementById('modal-text').value;
  const rem=500-text.length;
  document.getElementById('char-count').textContent=rem;
  const pct=Math.max(0,text.length/500);
  const color=rem<0?'#ef4444':rem<50?'#f59e0b':'var(--blue)';
  document.getElementById('char-ring').style.strokeDashoffset=75.4-pct*75.4;
  document.getElementById('char-ring').style.stroke=color;
  document.getElementById('char-count').style.color=rem<50?color:'';
}
async function submitPost() {
  if (!me) return;
  const content=document.getElementById('modal-text').value.trim();
  if (!content&&mediaFiles.length===0) return;
  if (content.length>500) { toast('Post too long.',true); return; }
  document.getElementById('modal-post-btn').disabled=true;
  try {
    const post=await api('POST','/api/posts',{content,parent_id:composeParentId||null});
    for (const file of mediaFiles) await uploadMedia(post.id,file);
    if (!composeParentId) {
      feedData.unshift(post);
      document.getElementById('feed-list').prepend(buildPostCard(post));
    }
    closeCompose(); toast('Posted!');
    if (composeParentId&&currentPage==='thread') loadThread(composeParentId);
  } catch(e) { toast(e.message,true); }
  document.getElementById('modal-post-btn').disabled=false;
}
function previewMedia() {
  const input=document.getElementById('media-input');
  mediaFiles=Array.from(input.files).slice(0,4);
  const prev=document.getElementById('media-preview');
  prev.innerHTML='';
  mediaFiles.forEach(file=>{
    const url=URL.createObjectURL(file);
    prev.innerHTML+=file.type.startsWith('video')
      ?`<video src="${url}" class="w-24 h-24 object-cover rounded-lg border border-gray-700"></video>`
      :`<img src="${url}" class="w-24 h-24 object-cover rounded-lg border border-gray-700">`;
  });
}
function renderInlineComposer() {
  const c=document.getElementById('inline-composer');
  const cf=document.getElementById('composer-filigree');
  if (!me) { c.classList.add('hidden'); cf.classList.add('hidden'); return; }
  c.classList.remove('hidden'); cf.classList.remove('hidden');
  c.innerHTML=`
    <div class="flex gap-3">
      <div class="flex-shrink-0 w-10 h-10 rounded-full flex items-center justify-center font-bold text-lg"
           style="background:${colorFor(me.username)}">${(me.display_name||'?')[0].toUpperCase()}</div>
      <div class="flex-1">
        <textarea id="inline-text" rows="2"
          class="w-full bg-transparent outline-none text-base resize-none placeholder-gray-600"
          placeholder="What's happening?!" onclick="openCompose()"></textarea>
        <div class="flex justify-between items-center pt-2 mt-2">
          <i class="fa-regular fa-image text-blue-400 text-sm"></i>
          <button onclick="openCompose()" class="btn-blue px-5 py-1 rounded-2xl font-bold text-sm">Post</button>
        </div>
      </div>
    </div>`;
}

// ═══════════════════════════════════════════════════════
// THREAD
// ═══════════════════════════════════════════════════════
async function loadThread(postId) {
  const c=document.getElementById('thread-content');
  c.innerHTML='<div class="px-4 py-8 text-center text-gray-500 text-sm">Loading…</div>';
  const post=feedData.find(p=>p.id===postId);
  if (!post) { c.innerHTML='<p class="px-4 py-8 text-center text-red-400 text-sm">Post not in cache.</p>'; return; }
  c.innerHTML='';
  c.appendChild(buildPostCard(post));
  if (post.children?.length) {
    const div=document.createElement('div');
    div.className='px-4 py-2 text-xs text-gray-500';
    div.textContent=`${post.children.length} ${post.children.length===1?'reply':'replies'}`;
    c.appendChild(div);
    c.appendChild(makeFigH());
    post.children.forEach(ch=>c.appendChild(buildPostCard(ch)));
  }
  if (me) {
    const rb=document.createElement('div');
    rb.className='px-4 py-4';
    rb.innerHTML=`<button onclick="replyTo('${post.id}')"
      class="w-full text-left text-gray-500 bg-gray-900 rounded-2xl px-4 py-3 hover:bg-gray-800 text-sm">
      Reply to this thread…</button>`;
    c.appendChild(rb);
  }
}

// ═══════════════════════════════════════════════════════
// SEARCH
// ═══════════════════════════════════════════════════════
async function doSearch(q) {
  const box=document.getElementById('search-results');
  if (!q.trim()) { box.innerHTML=''; return; }
  box.innerHTML='<p class="text-gray-500 text-sm py-4">Searching…</p>';
  try {
    const res=await api('GET',`/api/search?q=${encodeURIComponent(q)}`);
    box.innerHTML='';
    if (res.users.length) {
      box.innerHTML+='<h3 class="font-bold text-sm text-gray-400 mb-3">People</h3>';
      res.users.forEach(u=>box.appendChild(buildUserRow(u)));
    }
    if (res.posts.length) {
      box.innerHTML+='<h3 class="font-bold text-sm text-gray-400 mt-5 mb-3">Posts</h3>';
      res.posts.forEach(p=>box.appendChild(buildPostCard(p)));
    }
    if (!res.users.length&&!res.posts.length)
      box.innerHTML='<p class="text-gray-500 text-sm py-4">No results.</p>';
  } catch(e) {
    box.innerHTML=`<p class="text-red-400 text-sm py-4">${e.message}</p>`;
  }
}

// ═══════════════════════════════════════════════════════
// PROFILE
// ═══════════════════════════════════════════════════════
async function loadProfile(username) {
  if (!username) { openAuth(); return; }
  const c=document.getElementById('profile-content');
  c.innerHTML='<div class="py-12 text-center text-gray-500 text-sm">Loading…</div>';
  try {
    const u=await api('GET',`/api/users/${username}`);
    const isMe=me?.username===username;
    c.innerHTML=`
      <div>
        <div class="h-28 bg-gradient-to-r from-blue-900 to-gray-900"></div>
        <div class="px-4 pb-4">
          <div class="flex items-end justify-between -mt-8 mb-4">
            <div class="w-20 h-20 rounded-full border-4 border-black flex items-center justify-center font-bold text-3xl"
                 style="background:${colorFor(u.username)}">${(u.display_name||'?')[0].toUpperCase()}</div>
            ${isMe
              ?`<button class="border border-gray-600 hover:border-white px-4 py-1.5 rounded-2xl text-sm font-bold">Edit profile</button>`
              :`<div class="flex gap-2">
                 <button onclick="nav('messages','${u.username}')"
                   class="border border-gray-700 hover:border-green-500 hover:text-green-400
                          px-4 py-1.5 rounded-2xl text-sm font-bold flex items-center gap-1">
                   <i class="fa-solid fa-lock text-xs"></i> Message
                 </button>
                 <button onclick="doFollow('${u.username}',this)"
                   class="btn-blue px-5 py-1.5 rounded-2xl font-bold text-sm">
                   ${u.is_following?'Following':'Follow'}
                 </button>
               </div>`
            }
          </div>
          <div class="font-bold text-xl">${esc(u.display_name)}</div>
          <div class="text-gray-500 text-sm">@${esc(u.username)}</div>
          ${u.bio?`<p class="mt-2 text-sm">${esc(u.bio)}</p>`:''}
          <div class="flex gap-5 mt-3 text-sm">
            <span><strong>${u.following_count}</strong><span class="text-gray-500"> Following</span></span>
            <span><strong>${u.follower_count}</strong><span class="text-gray-500"> Followers</span></span>
          </div>
        </div>
      </div>`;
    c.appendChild(makeFigH());
    const pp=document.createElement('div');
    pp.id='profile-posts';
    if (!u.posts?.length) pp.innerHTML='<p class="text-center text-gray-600 py-12 text-sm">No posts yet.</p>';
    c.appendChild(pp);
    (u.posts||[]).forEach(p=>pp.appendChild(buildPostCard(p)));
  } catch(e) {
    c.innerHTML=`<p class="px-4 py-8 text-red-400 text-sm">${e.message}</p>`;
  }
}
async function doFollow(username,btn) {
  if (!requireAuth()) return;
  try {
    const res=await api('POST',`/api/users/${username}/follow`);
    btn.textContent=res.following?'Following':'Follow';
    toast(res.following?`Following @${username}`:`Unfollowed @${username}`);
  } catch(e) { toast(e.message,true); }
}

// ═══════════════════════════════════════════════════════
// DM — INBOX
// ═══════════════════════════════════════════════════════
async function loadInbox() {
  if (!me||!token) { openAuth(); return; }
  const list=document.getElementById('inbox-list');
  list.innerHTML='<p class="px-4 py-6 text-sm text-gray-500 text-center">Loading…</p>';
  try {
    const convs=await api('GET','/api/dm/inbox');
    list.innerHTML='';
    if (!convs.length) {
      list.innerHTML=`
        <div class="flex flex-col items-center justify-center py-16 px-8 text-center gap-4">
          <div class="w-16 h-16 rounded-2xl bg-gray-800 border border-gray-700 flex items-center justify-center">
            <i class="fa-solid fa-lock text-gray-500 text-2xl"></i>
          </div>
          <div>
            <p class="font-semibold text-white text-sm mb-1">No messages yet</p>
            <p class="text-xs text-gray-500">When someone sends you an encrypted message,<br>their name will appear here.</p>
          </div>
        </div>`;
      return;
    }
    let totalUnread=0;
    convs.forEach(conv=>{
      totalUnread+=conv.unread;
      const div=document.createElement('div');
      div.className='flex items-center gap-3 px-4 py-4 hover:bg-gray-900 cursor-pointer';
      div.onclick=()=>openConversation(conv.with);
      div.innerHTML=`
        <div class="w-10 h-10 rounded-full flex items-center justify-center font-bold flex-shrink-0 relative"
             style="background:${colorFor(conv.with)}">
          ${(conv.display_name||'?')[0].toUpperCase()}
          <div class="absolute -bottom-0.5 -right-0.5 w-4 h-4 bg-green-600 rounded-full flex items-center justify-center">
            <i class="fa-solid fa-lock text-white" style="font-size:6px"></i>
          </div>
        </div>
        <div class="flex-1 min-w-0">
          <div class="flex items-center justify-between">
            <span class="font-bold text-sm">${esc(conv.display_name)}</span>
            <span class="text-xs text-gray-500">${timeAgo(conv.updated_at)}</span>
          </div>
          <div class="flex items-center gap-1 mt-0.5">
            <i class="fa-solid fa-lock text-green-400 text-xs"></i>
            <span class="text-xs text-gray-500 truncate">
              ${conv.unread?`${conv.unread} new encrypted message${conv.unread>1?'s':''}`:'Encrypted conversation'}
            </span>
            ${conv.unread?`<span class="ml-auto bg-blue-500 text-white text-xs px-1.5 py-0.5 rounded-full font-bold">${conv.unread}</span>`:''}
          </div>
        </div>`;
      list.appendChild(div);
      list.appendChild(makeFigH());
    });
    updateDMBadge(totalUnread);
  } catch(e) {
    list.innerHTML=`<p class="px-4 py-4 text-red-400 text-sm">${e.message}</p>`;
  }
}
function updateDMBadge(n) {
  const badge=document.getElementById('dm-badge');
  if (n>0) { badge.textContent=n>9?'9+':n; badge.classList.remove('hidden'); }
  else badge.classList.add('hidden');
}

// ═══════════════════════════════════════════════════════
// DM — CONVERSATION
// ═══════════════════════════════════════════════════════
async function openConversation(username) {
  currentConvUser=username; convMessages=[];
  document.getElementById('inbox-view').classList.add('hidden');
  const cv=document.getElementById('conversation-view');
  cv.classList.remove('hidden'); cv.style.display='flex'; cv.style.flexDirection='column';
  document.getElementById('conv-title').textContent=`@${username}`;
  const cached=dmPassphraseCache[username]||'';
  document.getElementById('conv-passphrase').value=cached;
  document.getElementById('conv-passphrase').placeholder=`Passphrase for conversation with @${username}`;
  if (cached) {
    document.getElementById('conv-explainer').classList.add('hidden');
    document.getElementById('conv-messages').innerHTML='<p class="text-center text-gray-500 text-sm py-4">Decrypting…</p>';
    await loadConversation(username);
  } else {
    document.getElementById('conv-explainer').classList.remove('hidden');
    document.getElementById('conv-messages').innerHTML=`
      <div class="flex flex-col items-center justify-center py-16 px-8 text-center gap-4">
        <div class="w-16 h-16 rounded-2xl bg-green-500/10 border border-green-500/20 flex items-center justify-center">
          <i class="fa-solid fa-lock text-green-400 text-2xl"></i>
        </div>
        <div>
          <p class="font-semibold text-white mb-1">Conversation with @${esc(username)}</p>
          <p class="text-sm text-gray-500 leading-relaxed">Enter the shared passphrase to decrypt.</p>
        </div>
      </div>`;
  }
}
async function loadConversation(username) {
  const pp=document.getElementById('conv-passphrase').value;
  if (!pp) { toast('Enter passphrase to decrypt.',true); return; }
  dmPassphraseCache[username]=pp;
  const container=document.getElementById('conv-messages');
  container.innerHTML='<p class="text-center text-gray-500 text-sm py-4">Decrypting…</p>';
  try {
    convMessages=await api('GET',`/api/dm/conversation/${username}?passphrase=${encodeURIComponent(pp)}`);
    document.getElementById('conv-explainer').classList.add('hidden');
    const hintMsg=[...convMessages].reverse().find(m=>m.hint);
    if (hintMsg) {
      document.getElementById('conv-hint-text').textContent=hintMsg.hint;
      document.getElementById('conv-hint').classList.remove('hidden');
    }
    renderMessages();
  } catch(e) {
    container.innerHTML=`<p class="text-center text-red-400 text-sm py-4">${e.message}</p>`;
  }
}
function renderMessages() {
  const container=document.getElementById('conv-messages');
  if (!convMessages.length) {
    container.innerHTML=`
      <div class="flex flex-col items-center justify-center py-16 px-8 text-center gap-3">
        <div class="w-12 h-12 rounded-2xl bg-blue-500/10 border border-blue-500/20 flex items-center justify-center">
          <i class="fa-solid fa-lock-open text-blue-400 text-lg"></i>
        </div>
        <div>
          <p class="font-semibold text-white text-sm mb-1">Conversation unlocked</p>
          <p class="text-xs text-gray-500">No messages yet.</p>
        </div>
      </div>`;
    return;
  }
  container.innerHTML='';
  convMessages.forEach(msg=>{
    const isMe=msg.sender===me?.username;
    const div=document.createElement('div');
    div.className=`flex ${isMe?'justify-end':'justify-start'}`;
    div.innerHTML=`
      <div class="max-w-xs lg:max-w-md">
        ${!isMe?`<div class="text-xs text-gray-500 mb-1 px-1">@${esc(msg.sender)}</div>`:''}
        <div class="px-4 py-3 text-sm ${isMe?'dm-bubble-me text-white':'dm-bubble-them text-white'}">
          ${msg.decrypt_ok===false
            ?`<span class="text-red-300 text-xs flex items-center gap-1"><i class="fa-solid fa-triangle-exclamation"></i> Wrong passphrase</span>`
            :`<span class="whitespace-pre-wrap break-words">${esc(msg.plaintext||'')}</span>`}
        </div>
        <div class="text-xs text-gray-600 mt-1 px-1 flex items-center gap-1 ${isMe?'justify-end':''}">
          <i class="fa-solid fa-lock text-green-500"></i>${timeAgo(msg.created_at)}
        </div>
      </div>`;
    container.appendChild(div);
  });
  container.scrollTop=container.scrollHeight;
}
async function sendDM() {
  if (!me) return;
  const pp=document.getElementById('conv-passphrase').value;
  const content=document.getElementById('dm-compose').value.trim();
  if (!pp) { toast('Enter passphrase first.',true); return; }
  if (!content) { toast('Message is empty.',true); return; }
  const btn=document.getElementById('dm-send-btn');
  btn.disabled=true; btn.innerHTML='<i class="fa-solid fa-spinner fa-spin"></i>';
  try {
    const res=await api('POST','/api/dm/send',{recipient:currentConvUser,plaintext:content,passphrase:pp,hint:''});
    document.getElementById('dm-compose').value='';
    if (!convMessages.find(m=>m.id===res.id)) {
      convMessages.push({...res,decrypt_ok:true}); renderMessages();
    }
  } catch(e) { toast(e.message,true); }
  btn.disabled=false; btn.innerHTML='<i class="fa-solid fa-lock"></i>';
}
function showInbox() {
  currentConvUser=null;
  const cv=document.getElementById('conversation-view');
  cv.style.display='none'; cv.classList.add('hidden');
  document.getElementById('inbox-view').classList.remove('hidden');
  loadInbox();
}

// ═══════════════════════════════════════════════════════
// DM — NEW CONVERSATION
// ═══════════════════════════════════════════════════════
function openNewDM() {
  document.getElementById('new-dm-modal').classList.remove('hidden');
  document.getElementById('new-dm-recipient').focus();
}
function closeNewDM() {
  document.getElementById('new-dm-modal').classList.add('hidden');
  document.getElementById('new-dm-error').classList.add('hidden');
}
async function startNewDM() {
  const recipient=document.getElementById('new-dm-recipient').value.trim();
  const passphrase=document.getElementById('new-dm-passphrase').value;
  const errEl=document.getElementById('new-dm-error');
  errEl.classList.add('hidden');
  if (!recipient) { showNewDMError('Enter a recipient username.'); return; }
  if (!passphrase) { showNewDMError('Enter a shared passphrase.'); return; }
  try {
    const res=await api('POST','/api/dm/validate-passphrase',{passphrase});
    if (!res.valid) { showNewDMError(res.error); return; }
  } catch(e) { showNewDMError(e.message); return; }
  dmPassphraseCache[recipient]=passphrase;
  closeNewDM(); nav('messages'); await openConversation(recipient);
}
function showNewDMError(msg) {
  const el=document.getElementById('new-dm-error');
  el.textContent=msg; el.classList.remove('hidden');
}

// ═══════════════════════════════════════════════════════
// SIDEBAR WIDGETS
// ═══════════════════════════════════════════════════════
function renderSidebarUser() {
  const el=document.getElementById('sidebar-user');
  const svgH=`<svg class="filigree-h mb-1" viewBox="0 0 400 32" preserveAspectRatio="none"><rect width="400" height="32" fill="url(#fig-h-pat)"/></svg>`;
  if (!me) {
    el.innerHTML=svgH+`<button onclick="openAuth()" class="btn-blue w-full py-2.5 rounded-2xl font-bold text-sm">Sign in</button>`;
    return;
  }
  el.innerHTML=svgH+`
    <div class="flex items-center gap-2">
      <div class="w-9 h-9 rounded-full flex items-center justify-center font-bold text-sm flex-shrink-0"
           style="background:${colorFor(me.username)}">${(me.display_name||'?')[0].toUpperCase()}</div>
      <div class="flex-1 min-w-0 hidden xl:block">
        <div class="font-bold text-sm truncate">${esc(me.display_name)}</div>
        <div class="text-gray-500 text-xs truncate">@${esc(me.username)}</div>
      </div>
      <button onclick="logout()" title="Sign out"
        class="hidden xl:flex text-gray-500 hover:text-white w-8 h-8 items-center justify-center rounded-full hover:bg-gray-800">
        <i class="fa-solid fa-right-from-bracket text-sm"></i>
      </button>
    </div>`;
}
function renderTrending() {
  const tags={};
  feedData.forEach(p=>{ (p.content.match(/#\w+/g)||[]).forEach(t=>{ tags[t]=(tags[t]||0)+1; }); });
  const sorted=Object.entries(tags).sort((a,b)=>b[1]-a[1]).slice(0,5);
  const list=document.getElementById('trending-list');
  if (!sorted.length) { list.innerHTML='<p class="px-4 pb-4 text-sm text-gray-600">No trends yet.</p>'; return; }
  let html='';
  sorted.forEach(([tag,count])=>{
    html+=`<div class="px-4 py-3 hover:bg-gray-800 cursor-pointer"
      onclick="nav('explore');document.getElementById('search-input').value='${esc(tag)}';doSearch('${esc(tag)}')">
      <div class="font-bold text-sm">${esc(tag)}</div>
      <div class="text-gray-500 text-xs">${count} post${count>1?'s':''}</div>
    </div>
    <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none"><rect width="400" height="32" fill="url(#fig-h-pat)"/></svg>`;
  });
  list.innerHTML=html;
}
function renderWhoToFollow() {
  const seen={};
  feedData.filter(p=>p.author!==me?.username).forEach(p=>{ seen[p.author]={username:p.author,display_name:p.display_name}; });
  const candidates=Object.values(seen).slice(0,4);
  const list=document.getElementById('follow-list');
  if (!candidates.length) { list.innerHTML='<p class="px-4 pb-4 text-sm text-gray-600">No suggestions.</p>'; return; }
  let html='';
  candidates.forEach(u=>{
    html+=`<div class="flex items-center gap-3 px-4 py-3 hover:bg-gray-800">
      <div class="w-9 h-9 rounded-full flex items-center justify-center font-bold text-sm flex-shrink-0"
           style="background:${colorFor(u.username)}">${(u.display_name||'?')[0].toUpperCase()}</div>
      <div class="flex-1 min-w-0 cursor-pointer" onclick="nav('profile','${u.username}')">
        <div class="font-bold text-sm truncate">${esc(u.display_name)}</div>
        <div class="text-gray-500 text-xs">@${esc(u.username)}</div>
      </div>
      <button onclick="doFollow('${u.username}',this)" class="btn-blue px-3 py-1 rounded-2xl text-xs font-bold flex-shrink-0">Follow</button>
    </div>
    <svg class="filigree-h" viewBox="0 0 400 32" preserveAspectRatio="none"><rect width="400" height="32" fill="url(#fig-h-pat)"/></svg>`;
  });
  list.innerHTML=html;
}
function buildUserRow(u) {
  const div=document.createElement('div');
  div.className='flex items-center gap-3 py-3 px-1 cursor-pointer hover:bg-gray-900 rounded-xl';
  div.innerHTML=`
    <div class="w-10 h-10 rounded-full flex items-center justify-center font-bold flex-shrink-0"
         style="background:${colorFor(u.username)}">${(u.display_name||'?')[0].toUpperCase()}</div>
    <div class="flex-1 min-w-0" onclick="nav('profile','${u.username}')">
      <div class="font-bold text-sm">${esc(u.display_name)}</div>
      <div class="text-gray-500 text-xs">@${esc(u.username)}</div>
      ${u.bio?`<div class="text-xs mt-0.5 text-gray-300 truncate">${esc(u.bio)}</div>`:''}
    </div>
    <button onclick="doFollow('${u.username}',this)" class="btn-blue px-4 py-1 rounded-2xl text-sm font-bold flex-shrink-0">Follow</button>`;
  return div;
}

// ═══════════════════════════════════════════════════════
// INFINITE SCROLL
// ═══════════════════════════════════════════════════════
window.addEventListener('scroll',()=>{
  if (currentPage!=='feed') return;
  if (window.innerHeight+scrollY>=document.body.offsetHeight-400) loadFeed();
});

// ═══════════════════════════════════════════════════════
// UTILITIES
// ═══════════════════════════════════════════════════════
function requireAuth() { if (!me) { openAuth(); return false; } return true; }
function colorFor(str) {
  let h=0; for (let c of (str||'x')) h=(Math.imul(31,h)+c.charCodeAt(0))|0;
  return `hsl(${Math.abs(h)%360},55%,35%)`;
}
function esc(s) {
  return String(s||'').replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;');
}
function linkify(text) {
  return text
    .replace(/(https?:\/\/[^\s<]+)/g,`<a href="$1" target="_blank" rel="noopener" class="text-blue-400 hover:underline" onclick="event.stopPropagation()">$1</a>`)
    .replace(/#(\w+)/g,`<span class="text-blue-400 cursor-pointer hover:underline" onclick="event.stopPropagation();nav('explore');document.getElementById('search-input').value='#$1';doSearch('#$1')">#$1</span>`)
    .replace(/@(\w+)/g,`<span class="text-blue-400 cursor-pointer hover:underline" onclick="event.stopPropagation();nav('profile','$1')">@$1</span>`);
}
function timeAgo(ts) {
  const d=Math.floor(Date.now()/1000-ts);
  if(d<60) return `${d}s`; if(d<3600) return `${Math.floor(d/60)}m`;
  if(d<86400) return `${Math.floor(d/3600)}h`;
  return new Date(ts*1000).toLocaleDateString();
}
let toastTimer=null;
function toast(msg,err=false) {
  const el=document.getElementById('toast');
  el.textContent=msg;
  el.className=`fixed bottom-6 left-1/2 -translate-x-1/2 px-6 py-3 rounded-2xl text-sm font-medium shadow-xl z-[100] text-white ${err?'bg-red-500':'bg-blue-500'}`;
  clearTimeout(toastTimer);
  toastTimer=setTimeout(()=>el.classList.add('hidden'),3000);
}
function showSkeleton(id,n) {
  const c=document.getElementById(id);
  for(let i=0;i<n;i++){
    const s=document.createElement('div');
    s.className='skeleton-item px-4 py-4 flex gap-3';
    s.innerHTML=`
      <div class="skeleton w-10 h-10 rounded-full flex-shrink-0"></div>
      <div class="flex-1 space-y-2">
        <div class="skeleton h-3 w-1/3 rounded"></div>
        <div class="skeleton h-3 w-full rounded"></div>
        <div class="skeleton h-3 w-4/5 rounded"></div>
      </div>`;
    c.appendChild(s);
  }
}
function removeSkeleton(id) { document.querySelectorAll(`#${id} .skeleton-item`).forEach(el=>el.remove()); }
function showError(id,msg) {
  const div=document.createElement('div');
  div.className='text-center py-12 text-red-400 text-sm';
  div.textContent=msg;
  document.getElementById(id).appendChild(div);
}

// ═══════════════════════════════════════════════════════
// SS SPEED PANEL
// ═══════════════════════════════════════════════════════
function toggleSSPanel() {
  const panel=document.getElementById('ss-speed-panel');
  const caret=document.getElementById('ss-panel-caret');
  const open=panel.classList.toggle('open');
  caret.className=`fa-solid ${open?'fa-chevron-down':'fa-chevron-up'} ml-auto text-gray-600 text-xs`;
}
function setSSTiming(secs) {
  document.querySelectorAll('.speed-btn').forEach(b=>{
    b.classList.toggle('active',parseInt(b.dataset.secs)===secs);
  });
  SS.IDLE_MS=secs===0?0:secs*1000;
  SS.resetIdleTimer();
  toast(secs===0?'Auto-lock disabled':`Auto-lock: ${secs<60?secs+'s':(secs/60)+'m'}`);
  document.getElementById('ss-speed-panel').classList.remove('open');
  document.getElementById('ss-panel-caret').className='fa-solid fa-chevron-up ml-auto text-gray-600 text-xs';
}

// ═══════════════════════════════════════════════════════
// SESSION LOCK — no password, just blocks + screensaver
// ═══════════════════════════════════════════════════════
function showLock() {
  document.getElementById('lock-overlay').classList.add('active');
}
function hideLock() {
  document.getElementById('lock-overlay').classList.remove('active');
}
function unlockSession() {
  // No password required — one click to resume
  hideLock();
  SS.stop();
}
function lockLogout() {
  hideLock(); SS.stop(); logout();
}

// ═══════════════════════════════════════════════════════
// BOOT
// ═══════════════════════════════════════════════════════
window.addEventListener('DOMContentLoaded', async ()=>{
  token=localStorage.getItem('token');
  me=JSON.parse(localStorage.getItem('me')||'null');
  renderSidebarUser();
  renderInlineComposer();
  connectWS();
  await loadFeed(true);
  renderTrending();
  renderWhoToFollow();
  document.getElementById('search-input').addEventListener('input',e=>{
    clearTimeout(searchTimer);
    searchTimer=setTimeout(()=>doSearch(e.target.value),300);
  });
  document.getElementById('rs-search').addEventListener('keydown',e=>{
    if(e.key==='Enter'){
      nav('explore');
      document.getElementById('search-input').value=e.target.value;
      doSearch(e.target.value);
    }
  });
  const params=new URLSearchParams(location.search);
  if(params.get('thread')) nav('thread',params.get('thread'));
  if(params.get('dm')) { nav('messages'); openConversation(params.get('dm')); }
  document.addEventListener('keydown',e=>{
    if(document.getElementById('lock-overlay').classList.contains('active')) return;
    if(['TEXTAREA','INPUT'].includes(document.activeElement.tagName)) return;
    if(e.key==='n') openCompose();
    if(e.key==='m') { if(me) nav('messages'); }
    if(e.key==='Escape') { closeCompose(); closeAuth(); closeNewDM(); }
  });
  setTimeout(()=>SS.init(),800);
});
</script>

<!-- ── SCREENSAVER ─────────────────────────────────────── -->
<canvas id="screensaver-canvas"
  class="hidden fixed inset-0 z-[200] cursor-none"
  style="background:#000; width:100vw; height:100vh;">
</canvas>

<script>
// ═══════════════════════════════════════════════════════
// SCREENSAVER — LSD Fluid port (C# → Canvas JS)
// Gravitational orbital comet + wave refraction + fluid
// ═══════════════════════════════════════════════════════

const SS = {
  canvas:    null,
  ctx:       null,
  active:    false,
  timer:     null,
  idleTimer: null,
  IDLE_MS:   2 * 60 * 1000,

  SW: 160, SH: 100,
  BW: 320, BH: 200,

  density: null, velX: null, velY: null,
  wave: null, wavePrev: null, waveNext: null,

  orbX: 0, orbY: 0, orbVX: 0, orbVY: 0.55,
  gravCX: 0, gravCY: 0,
  flamePhase: 0,
  frameTime: 0,

  palette: null,

  init() {
    this.canvas = document.getElementById('screensaver-canvas');
    this.ctx    = this.canvas.getContext('2d');

    this.palette = [];
    for (let i = 0; i < 128; i++) {
      const hue = (i / 128) * 360;
      this.palette.push(this.hslToRgb(hue, 1, 0.5));
    }

    this.reset();
    this.bindIdleEvents();
    this.resetIdleTimer();
  },

  reset() {
    const {SW, SH, BW, BH} = this;

    this.density  = new Float32Array(SW * SH);
    this.velX     = new Float32Array(SW * SH);
    this.velY     = new Float32Array(SW * SH);
    this.wave     = new Float32Array(BW * BH);
    this.wavePrev = new Float32Array(BW * BH);
    this.waveNext = new Float32Array(BW * BH);

    for (let y = 0; y < SH; y++) {
      for (let x = 0; x < SW; x++) {
        const fx = x / SW, fy = y / SH;
        this.density[y * SW + x] = Math.sin(fx * 10 + fy * 10) * 1.5 + 0.5;
        this.velX[y * SW + x]    = Math.sin(fx * 15) * 0.1;
        this.velY[y * SW + x]    = Math.cos(fy * 15) * 0.1;
      }
    }

    this.gravCX = SW * 0.5;
    this.gravCY = SH * 0.5;
    this.orbX   = this.gravCX + SW * 0.28;
    this.orbY   = this.gravCY - SH * 0.08;
    this.orbVX  = 0;
    this.orbVY  = 0.55;
    this.flamePhase = 0;
    this.frameTime  = 0;
  },

  hslToRgb(h, s, l) {
    h /= 360;
    let r, g, b;
    if (s === 0) { r = g = b = l; }
    else {
      const hue2rgb = (p, q, t) => {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1/6) return p + (q - p) * 6 * t;
        if (t < 1/2) return q;
        if (t < 2/3) return p + (q - p) * (2/3 - t) * 6;
        return p;
      };
      const q = l < 0.5 ? l * (1 + s) : l + s - l * s;
      const p = 2 * l - q;
      r = hue2rgb(p, q, h + 1/3);
      g = hue2rgb(p, q, h);
      b = hue2rgb(p, q, h - 1/3);
    }
    return [Math.round(r*255), Math.round(g*255), Math.round(b*255)];
  },

  // ── WAVE — FIX 4: depression-plus-crown impact ──────
  spawnRainImpact() {
    const {BW, BH} = this;
    // FIX 7: randomized droplet sizes, small vastly outnumber large
    const radius = 1.5 + Math.random() * Math.random() * 8;
    const energy = radius * radius * 0.8;

    const x = Math.floor(Math.random() * (BW - 4)) + 2;
    const y = Math.floor(Math.random() * (BH - 4)) + 2;

    // FIX 4: center depression
    this.wave[y * BW + x] -= energy;

    // FIX 4: crown ring at radius ~3
    for (let a = 0; a < Math.PI * 2; a += 0.2) {
      const px = Math.round(x + Math.cos(a) * 3);
      const py = Math.round(y + Math.sin(a) * 3);
      if (px > 1 && py > 1 && px < BW - 1 && py < BH - 1) {
        this.wave[py * BW + px] += energy * 0.35;
      }
    }
  },

  // ── WAVE — FIX 5: 8-neighbor stencil + FIX 6: surface tension ──
  updateWaves() {
    const {BW, BH} = this;
    const damp = 0.985, clamp = 24;

    for (let y = 1; y < BH - 1; y++) {
      for (let x = 1; x < BW - 1; x++) {
        const i  = y * BW + x;
        const N  = this.wave[(y-1)*BW + x];
        const S  = this.wave[(y+1)*BW + x];
        const E  = this.wave[y*BW + (x+1)];
        const W  = this.wave[y*BW + (x-1)];
        const NE = this.wave[(y-1)*BW + (x+1)];
        const NW = this.wave[(y-1)*BW + (x-1)];
        const SE = this.wave[(y+1)*BW + (x+1)];
        const SW2= this.wave[(y+1)*BW + (x-1)];

        // FIX 5: 8-neighbor stencil for circular ripples
        let v = (N + S + E + W) * 0.30
              + (NE + NW + SE + SW2) * 0.175
              - this.wavePrev[i];
        v *= damp;
        v = Math.max(-clamp, Math.min(clamp, v));

        // FIX 6: surface tension Laplacian sharpening
        v += (W + E + N + S - this.wave[i] * 4) * 0.015;

        this.waveNext[i] = v;
      }
    }

    const tmp    = this.wavePrev;
    this.wavePrev = this.wave;
    this.wave     = this.waveNext;
    this.waveNext = tmp;
  },

  // ── RENDER FRAME ────────────────────────────────────
  frame() {
    const {SW, SH, BW, BH} = this;
    const t  = this.frameTime * 0.35;
    const tt = this.frameTime * 0.003;

    // ── Orbital gravity ──
    const GM=18, soft=4, drag=0.9995;
    const dxG = this.gravCX - this.orbX;
    const dyG = this.gravCY - this.orbY;
    const distSq  = dxG*dxG + dyG*dyG + soft*soft;
    const distInv = 1 / Math.sqrt(distSq);
    const force   = GM * distInv * distInv;
    this.orbVX += dxG * distInv * force;
    this.orbVY += dyG * distInv * force;
    this.orbVX *= drag; this.orbVY *= drag;
    this.orbX  += this.orbVX;
    this.orbY  += this.orbVY;

    const margin = 6;
    if (this.orbX < margin)    { this.orbX=margin;    this.orbVX= Math.abs(this.orbVX)*0.6; }
    if (this.orbX > SW-margin) { this.orbX=SW-margin; this.orbVX=-Math.abs(this.orbVX)*0.6; }
    if (this.orbY < margin)    { this.orbY=margin;    this.orbVY= Math.abs(this.orbVY)*0.6; }
    if (this.orbY > SH-margin) { this.orbY=SH-margin; this.orbVY=-Math.abs(this.orbVY)*0.6; }

    const cx = Math.floor(this.orbX);
    const cy = Math.floor(this.orbY);

    // ── Flame injection ──
    const radius = 8;
    for (let dy = -radius; dy <= radius; dy++) {
      for (let dx = -radius; dx <= radius; dx++) {
        const nx = cx+dx, ny = cy+dy;
        if (nx<0||ny<0||nx>=SW||ny>=SH) continue;
        const dist  = Math.sqrt(dx*dx+dy*dy);
        const angle = Math.atan2(dy, dx);
        const edge  = radius
          + Math.sin(angle*5  + this.flamePhase)     * 2.5
          + Math.sin(angle*9  - this.flamePhase*1.7) * 1.5
          + Math.sin(angle*13 + this.flamePhase*0.7) * 0.8;
        if (dist > edge) continue;
        const falloff = Math.pow(1 - dist/edge, 2);
        this.density[ny*SW+nx] += 0.8 * falloff;
        const swirl = Math.sin(angle*7 + this.flamePhase*3);
        this.velX[ny*SW+nx] += -dy * 0.03 * swirl * falloff;
        this.velY[ny*SW+nx] +=  dx * 0.03 * swirl * falloff;
      }
    }

    // ── Vortex ──
    for (let dy = -10; dy <= 10; dy++) {
      for (let dx = -10; dx <= 10; dx++) {
        const nx=cx+dx, ny=cy+dy;
        if (nx<1||ny<1||nx>=SW-1||ny>=SH-1) continue;
        const r2 = dx*dx+dy*dy;
        if (r2>100) continue;
        const inv = 1/(1+r2*0.08);
        this.velX[ny*SW+nx] += -dy * inv * 0.05;
        this.velY[ny*SW+nx] +=  dx * inv * 0.05;
      }
    }
    this.flamePhase += 0.18;

    // ── Fluid advection ──
    const newDensity = new Float32Array(SW * SH);
    for (let y = 1; y < SH-1; y++) {
      for (let x = 1; x < SW-1; x++) {
        const i  = y*SW+x;
        const px = x - this.velX[i];
        const py = y - this.velY[i];
        const ix = Math.max(1, Math.min(SW-2, Math.floor(px)));
        const iy = Math.max(1, Math.min(SH-2, Math.floor(py)));
        const fx = px-ix, fy = py-iy;
        const d =
          (1-fx)*(1-fy)*this.density[iy*SW+ix]       +
          fx    *(1-fy)*this.density[iy*SW+ix+1]     +
          (1-fx)*fy    *this.density[(iy+1)*SW+ix]   +
          fx    *fy    *this.density[(iy+1)*SW+ix+1];
        newDensity[i] = Math.max(0, Math.min(1, d));
      }
    }
    this.density = newDensity;

    // ── Diffusion ──
    for (let y = 1; y < SH-1; y++) {
      for (let x = 1; x < SW-1; x++) {
        const i = y*SW+x;
        this.density[i] = (this.density[i] +
          (this.density[i-1]+this.density[i+1]+
           this.density[(y-1)*SW+x]+this.density[(y+1)*SW+x])*0.2)*0.5;
      }
    }

    // ── Draw to canvas ──
    this.canvas.width  = BW;
    this.canvas.height = BH;

    // FIX 1+2: pre-render background into its own buffer
    const background = new Uint8ClampedArray(BW * BH * 4);
    const cx2 = BW / 2, cy2 = BH / 2;
    const maxR = Math.sqrt(cx2*cx2 + cy2*cy2);

    for (let y = 0; y < BH; y++) {
      for (let x = 0; x < BW; x++) {
        const tv = Math.min(Math.sqrt((x-cx2)**2 + (y-cy2)**2) / maxR, 1);

        let bgR = Math.sin(tt)   * 127 + 128 + (Math.cos(tt)   * 127 + 128 - (Math.sin(tt)   * 127 + 128)) * tv;
        let bgG = Math.sin(tt+2) * 127 + 128 + (Math.cos(tt+2) * 127 + 128 - (Math.sin(tt+2) * 127 + 128)) * tv;
        let bgB = Math.sin(tt+4) * 127 + 128 + (Math.cos(tt+4) * 127 + 128 - (Math.sin(tt+4) * 127 + 128)) * tv;

        // FIX 8: fine procedural texture overlay so refraction is visible
        bgR += Math.sin(x * 0.09 + tt) * 6;
        bgG += Math.sin(y * 0.11 + tt) * 6;
        bgB += Math.sin((x + y) * 0.07 + tt) * 6;

        const p = (y * BW + x) * 4;
        background[p+0] = Math.max(0, Math.min(255, bgR));
        background[p+1] = Math.max(0, Math.min(255, bgG));
        background[p+2] = Math.max(0, Math.min(255, bgB));
        background[p+3] = 255;
      }
    }

    // ── Composite: wave refraction + fluid blend ──
    const imgData = this.ctx.createImageData(BW, BH);
    const pix = imgData.data;

    for (let y = 0; y < BH; y++) {
      for (let x = 0; x < BW; x++) {
        // FIX 3: wave normal → refracted coords → actually sample background
        const wIdx = y * BW + x;
        const wnx  = (x < BW-1 ? this.wave[wIdx+1]        : 0) - (x > 0 ? this.wave[wIdx-1]        : 0);
        const wny  = (y < BH-1 ? this.wave[(y+1)*BW+x]    : 0) - (y > 0 ? this.wave[(y-1)*BW+x]    : 0);
        const rx   = Math.max(0, Math.min(BW-1, x + Math.floor(wnx * 1.8)));
        const ry   = Math.max(0, Math.min(BH-1, y + Math.floor(wny * 1.8)));

        // FIX 3: sample the background at the REFRACTED position
        const rp = (ry * BW + rx) * 4;
        const refBg = {
          r: background[rp+0],
          g: background[rp+1],
          b: background[rp+2]
        };

        // Fluid density sample (bilinear)
        const sxf = x * (SW-1) / (BW-1);
        const syf = y * (SH-1) / (BH-1);
        const sx0 = Math.floor(sxf), sy0 = Math.floor(syf);
        const sx1 = Math.min(sx0+1, SW-1), sy1 = Math.min(sy0+1, SH-1);
        const sfx = sxf - sx0, sfy = syf - sy0;
        const d =
          this.density[sy0*SW+sx0]*(1-sfx)*(1-sfy) +
          this.density[sy0*SW+sx1]*sfx    *(1-sfy) +
          this.density[sy1*SW+sx0]*(1-sfx)*sfy     +
          this.density[sy1*SW+sx1]*sfx    *sfy;

        const ci    = Math.floor((d + 0.2 * Math.sin(t * 0.05)) * 127) & 127;
        const pal   = this.palette[ci];
        const alpha = Math.max(0, Math.min(1, d));

        const pi = (y * BW + x) * 4;
        pix[pi+0] = Math.floor(pal[0] * alpha + refBg.r * (1 - alpha));
        pix[pi+1] = Math.floor(pal[1] * alpha + refBg.g * (1 - alpha));
        pix[pi+2] = Math.floor(pal[2] * alpha + refBg.b * (1 - alpha));
        pix[pi+3] = 255;
      }
    }

    this.ctx.putImageData(imgData, 0, 0);
    this.canvas.style.width  = '100vw';
    this.canvas.style.height = '100vh';
    this.canvas.style.imageRendering = 'pixelated';

    if (Math.random() < 0.4) this.spawnRainImpact();
    this.updateWaves();
    this.frameTime++;
  },

  // ── LIFECYCLE ───────────────────────────────────────
  start() {
    if (this.active) return;
    this.active = true;
    this.reset();
    this.canvas.classList.remove('hidden');
    document.body.style.cursor = 'none';
    this.timer = setInterval(() => SS.frame(), 44);
  },

  stop() {
    if (!this.active) return;
    this.active = false;
    clearInterval(this.timer);
    this.canvas.classList.add('hidden');
    document.body.style.cursor = '';
    this.resetIdleTimer();
  },

  resetIdleTimer() {
    clearTimeout(this.idleTimer);
    this.idleTimer = setTimeout(() => SS.start(), this.IDLE_MS);
  },

  bindIdleEvents() {
    const reset = () => {
      if (SS.active) SS.stop();
      else SS.resetIdleTimer();
    };
    ['mousemove','mousedown','keydown','touchstart','scroll','click']
      .forEach(ev => document.addEventListener(ev, reset, {passive:true}));

    this.canvas.addEventListener('click',      () => SS.stop());
    this.canvas.addEventListener('touchstart', () => SS.stop(), {passive:true});
    document.addEventListener('keydown',       () => SS.active && SS.stop());
  }
};

window.addEventListener('DOMContentLoaded', () => {
  setTimeout(() => SS.init(), 1000);
});
</script>
<!-- ═══════════════════════════════════════════════════════
     SCREENSAVER ENGINE REGISTRY + SELECTOR
     Drop this immediately after the closing </script> tag
     of the existing SS block.
     ═══════════════════════════════════════════════════════ -->
<script>
// ─────────────────────────────────────────────────────────
// LSD PALETTE (shared across all four CS engines)
// 128 entries × 3 bytes, 6-bit (0-63) → 8-bit (×255/63)
// ─────────────────────────────────────────────────────────
const LSD_PAL_RAW = new Uint8Array([
  0x3f,0x00,0x3f, 0x3f,0x02,0x3d, 0x3f,0x04,0x3b, 0x3f,0x06,0x39,
  0x3f,0x08,0x37, 0x3f,0x0a,0x35, 0x3f,0x0c,0x33, 0x3f,0x0e,0x31,
  0x3f,0x10,0x2f, 0x3f,0x12,0x2d, 0x3f,0x14,0x2b, 0x3f,0x16,0x29,
  0x3f,0x18,0x27, 0x3f,0x1a,0x25, 0x3f,0x1c,0x23, 0x3f,0x1e,0x21,
  0x3f,0x20,0x1f, 0x3f,0x22,0x1d, 0x3f,0x24,0x1b, 0x3f,0x26,0x19,
  0x3f,0x28,0x17, 0x3f,0x2a,0x15, 0x3f,0x2c,0x13, 0x3f,0x2e,0x11,
  0x3f,0x30,0x0f, 0x3f,0x32,0x0d, 0x3f,0x34,0x0b, 0x3f,0x36,0x09,
  0x3f,0x38,0x07, 0x3f,0x3a,0x05, 0x3f,0x3c,0x03, 0x3f,0x3e,0x01,
  0x3f,0x3f,0x00, 0x3d,0x3f,0x02, 0x3b,0x3f,0x04, 0x39,0x3f,0x06,
  0x37,0x3f,0x08, 0x35,0x3f,0x0a, 0x33,0x3f,0x0c, 0x31,0x3f,0x0e,
  0x2f,0x3f,0x10, 0x2d,0x3f,0x12, 0x2b,0x3f,0x14, 0x29,0x3f,0x16,
  0x27,0x3f,0x18, 0x25,0x3f,0x1a, 0x23,0x3f,0x1c, 0x21,0x3f,0x1e,
  0x1f,0x3f,0x20, 0x1d,0x3f,0x22, 0x1b,0x3f,0x24, 0x19,0x3f,0x26,
  0x17,0x3f,0x28, 0x15,0x3f,0x2a, 0x13,0x3f,0x2c, 0x11,0x3f,0x2e,
  0x0f,0x3f,0x30, 0x0d,0x3f,0x32, 0x0b,0x3f,0x34, 0x09,0x3f,0x36,
  0x07,0x3f,0x38, 0x05,0x3f,0x3a, 0x03,0x3f,0x3c, 0x01,0x3f,0x3e,
  0x00,0x3f,0x3f, 0x00,0x3d,0x3f, 0x00,0x3b,0x3f, 0x00,0x39,0x3f,
  0x00,0x37,0x3f, 0x00,0x35,0x3f, 0x00,0x33,0x3f, 0x00,0x31,0x3f,
  0x00,0x2f,0x3f, 0x00,0x2d,0x3f, 0x00,0x2b,0x3f, 0x00,0x29,0x3f,
  0x00,0x27,0x3f, 0x00,0x25,0x3f, 0x00,0x23,0x3f, 0x00,0x21,0x3f,
  0x00,0x1f,0x3f, 0x00,0x1d,0x3f, 0x00,0x1b,0x3f, 0x00,0x19,0x3f,
  0x00,0x17,0x3f, 0x00,0x15,0x3f, 0x00,0x13,0x3f, 0x00,0x11,0x3f,
  0x00,0x0f,0x3f, 0x00,0x0d,0x3f, 0x00,0x0b,0x3f, 0x00,0x09,0x3f,
  0x00,0x07,0x3f, 0x00,0x05,0x3f, 0x00,0x03,0x3f, 0x00,0x01,0x3f,
  0x00,0x00,0x3f, 0x02,0x00,0x3f, 0x04,0x00,0x3f, 0x06,0x00,0x3f,
  0x08,0x00,0x3f, 0x0a,0x00,0x3f, 0x0c,0x00,0x3f, 0x0e,0x00,0x3f,
  0x10,0x00,0x3f, 0x12,0x00,0x3f, 0x14,0x00,0x3f, 0x16,0x00,0x3f,
  0x18,0x00,0x3f, 0x1a,0x00,0x3f, 0x1c,0x00,0x3f, 0x1e,0x00,0x3f,
  0x20,0x00,0x3f, 0x22,0x00,0x3f, 0x24,0x00,0x3f, 0x26,0x00,0x3f,
  0x28,0x00,0x3f, 0x2a,0x00,0x3f, 0x2c,0x00,0x3f, 0x2e,0x00,0x3f,
  0x30,0x00,0x3f, 0x32,0x00,0x3f, 0x34,0x00,0x3f, 0x36,0x00,0x3f,
  0x38,0x00,0x3f, 0x3a,0x00,0x3f, 0x3c,0x00,0x3f, 0x3f,0x00,0x3f
]);

// Expand to [r,g,b] per entry, 8-bit
const LSD_PAL = [];
for (let i = 0; i < 128; i++) {
  LSD_PAL.push([
    (LSD_PAL_RAW[i*3+0] * 255 / 63) | 0,
    (LSD_PAL_RAW[i*3+1] * 255 / 63) | 0,
    (LSD_PAL_RAW[i*3+2] * 255 / 63) | 0
  ]);
}

// Flat Uint32 RGBA lookup for direct pixel writes (LE: ABGR in memory = RGBA in Uint32)
const LSD_PAL32 = new Uint32Array(128);
for (let i = 0; i < 128; i++) {
  const [r,g,b] = LSD_PAL[i];
  LSD_PAL32[i] = (0xFF000000) | (b << 16) | (g << 8) | r;
}

// ─────────────────────────────────────────────────────────
// LOOKUP TABLE (shared, identical across all four CS files)
// ─────────────────────────────────────────────────────────
const LSD_LUT = new Uint8Array([
  0x40,0x40,0x40,0x40,0x40,0x40,0x40,0x40,
  0x3f,0x3f,0x3f,0x3f,0x3f,0x3e,0x3e,0x3e,
  0x3e,0x3d,0x3d,0x3d,0x3c,0x3c,0x3b,0x3b,
  0x3a,0x3a,0x39,0x39,0x38,0x38,0x37,0x37,
  0x36,0x35,0x35,0x34,0x34,0x33,0x32,0x32,
  0x31,0x30,0x30,0x2f,0x2e,0x2e,0x2d,0x2c,
  0x2c,0x2b,0x2a,0x29,0x29,0x28,0x27,0x26,
  0x25,0x25,0x24,0x23,0x22,0x22,0x21,0x20,
  0x1f,0x1e,0x1e,0x1d,0x1c,0x1b,0x1b,0x1a,
  0x19,0x18,0x17,0x17,0x16,0x15,0x14,0x14,
  0x13,0x12,0x12,0x11,0x10,0x10,0x0f,0x0e,
  0x0e,0x0d,0x0c,0x0c,0x0b,0x0b,0x0a,0x09,
  0x09,0x08,0x08,0x07,0x07,0x06,0x06,0x05,
  0x05,0x05,0x04,0x04,0x03,0x03,0x03,0x02,
  0x02,0x02,0x02,0x01,0x01,0x01,0x01,0x01,
  0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
]);
const LSD_LUT_LEN = LSD_LUT.length; // 128

// ─────────────────────────────────────────────────────────
// ENGINE: SS1 — LSDscreensaver1.cs port
// 80×50 grid → 4×4 pixel blocks → 320×200
// Original per-block var68a/b increment, per-row c/d
// ─────────────────────────────────────────────────────────
const SS1 = {
  name: 'LSD·1',
  W: 320, H: 200,
  var68a:0, var68b:0, var68c:0, var68d:0,
  vars505: new Uint8Array([2,1,3,4]),
  bpVar: 0,
  vars: new Uint8Array(320*200),

  reset() {
    this.var68a=0; this.var68b=0; this.var68c=0; this.var68d=0;
    this.vars505.set([2,1,3,4]);
    this.bpVar=0;
    this.vars.fill(0);
  },

  frame(pix32) {
    const {W, H, vars, vars505} = this;
    const LUT = LSD_LUT, LUTL = LSD_LUT_LEN;
    let {var68a:a, var68b:b, var68c:c, var68d:d, bpVar:bp} = this;
    const bpLo = bp & 0xFF;

    // 80×50 grid, each cell → 4×4 pixels
    for (let gy = 0; gy < 50; gy++) {
      for (let gx = 0; gx < 80; gx++) {
        let al = bpLo;
        al = (al + LUT[gy % LUTL]) & 0xFF;
        al = (al + LUT[gx % LUTL]) & 0xFF;
        al = (al + LUT[(gy+2) % LUTL]) & 0xFF;
        al = (al + LUT[(gx+1) % LUTL]) & 0xFF;
        al = (al | 0x80) & 0xFF;
        const ci = (al - 128) & 0x7F;
        const color = LSD_PAL32[ci];

        for (let dy = 0; dy < 4; dy++) {
          for (let dx = 0; dx < 4; dx++) {
            const px = gx*4+dx, py = gy*4+dy;
            if (px < W && py < H) {
              vars[py*W+px] = ci;
              pix32[py*W+px] = color;
            }
          }
        }
        a = (a+1) & 0xFF;
        b = (b+3) & 0xFF;
      }
      c = (c+2) & 0xFF;
      d = (d+1) & 0xFF;
    }

    // animation update
    bp = (bp - 1) & 0xFFFF;
    let bl = bp & 0xFF;
    bl = (bl ^ ((bp >> 8) & 0xFF)) & 0xFF;
    bl = (bl ^ vars[199*W+319]) & 0xFF;
    bl = (bl ^ c) & 0xFF;
    bl = (bl ^ a) & 0xFF;
    bl = (bl + d) & 0xFF;
    bl = (bl + b) & 0xFF;
    if ((bl & 0x08) === 0) { const di=bl&3; if(vars505[di]<0xFD) vars505[di]++; }
    else                   { const di=bl&3; if(vars505[di]>0x03) vars505[di]--; }
    a = (a + vars505[0]) & 0xFF;
    b = (b - vars505[1] + 256) & 0xFF;
    c = (c + vars505[2]) & 0xFF;
    d = (d - vars505[3] + 256) & 0xFF;

    this.var68a=a; this.var68b=b; this.var68c=c; this.var68d=d; this.bpVar=bp;
  }
};

// ─────────────────────────────────────────────────────────
// ENGINE: SS2 — LSDscreensaver2.cs port
// 1280×800 internal grid downsampled 4×4 by RGB averaging
// Produces sub-pixel anti-aliased smooth version
// ─────────────────────────────────────────────────────────
const SS2 = {
  name: 'LSD·2',
  W: 320, H: 200,
  GW: 1280, GH: 800,
  var68a:0, var68b:0, var68c:0, var68d:0,
  vars505: new Uint8Array([2,1,3,4]),
  bpVar: 0,
  vars: new Uint8Array(1280*800),

  reset() {
    this.var68a=0; this.var68b=0; this.var68c=0; this.var68d=0;
    this.vars505.set([2,1,3,4]);
    this.bpVar=0;
    this.vars.fill(0);
  },

  frame(pix32) {
    const {W, H, GW, GH, vars, vars505} = this;
    const LUT = LSD_LUT, LUTL = LSD_LUT_LEN;
    let {var68a:a, var68b:b, var68c:c, var68d:d, bpVar:bp} = this;
    const bpLo = bp & 0xFF;

    // iterate 1280×800 at 4-pixel strides, filling sub-grid then downsampling
    for (let y = 0; y < GH; y += 4) {
      for (let x = 0; x < GW; x += 4) {
        // fill 4×4 sub-block
        for (let dy = 0; dy < 4; dy++) {
          for (let dx = 0; dx < 4; dx++) {
            const sx = x+dx, sy = y+dy;
            let al = bpLo;
            al = (al + LUT[sy % LUTL]) & 0xFF;
            al = (al + LUT[sx % LUTL]) & 0xFF;
            al = (al + LUT[(sy+2) % LUTL]) & 0xFF;
            al = (al + LUT[(sx+1) % LUTL]) & 0xFF;
            al = (al | 0x80) & 0xFF;
            vars[sy*GW+sx] = (al - 128) & 0x7F;
          }
        }
        // downsample 4×4 → 1 pixel by averaging RGB
        const px = x >> 2, py = y >> 2;
        if (px < W && py < H) {
          let rS=0, gS=0, bS=0;
          for (let dy = 0; dy < 4; dy++) {
            for (let dx = 0; dx < 4; dx++) {
              const ci = vars[(y+dy)*GW+(x+dx)];
              rS += LSD_PAL[ci][0];
              gS += LSD_PAL[ci][1];
              bS += LSD_PAL[ci][2];
            }
          }
          const r=(rS/16)|0, g=(gS/16)|0, bv=(bS/16)|0;
          pix32[py*W+px] = (0xFF000000) | (bv<<16) | (g<<8) | r;
        }
        a = (a+1) & 0xFF;
        b = (b+3) & 0xFF;
      }
      c = (c+2) & 0xFF;
      d = (d+1) & 0xFF;
    }

    bp = (bp - 1) & 0xFFFF;
    let bl = bp & 0xFF;
    bl = (bl ^ ((bp >> 8) & 0xFF)) & 0xFF;
    bl = (bl ^ vars[799*GW+1279]) & 0xFF;
    bl = (bl ^ c) & 0xFF;
    bl = (bl ^ a) & 0xFF;
    bl = (bl + d) & 0xFF;
    bl = (bl + b) & 0xFF;
    if ((bl & 0x08) === 0) { const di=bl&3; if(vars505[di]<0xFD) vars505[di]++; }
    else                   { const di=bl&3; if(vars505[di]>0x03) vars505[di]--; }
    a = (a + vars505[0]) & 0xFF;
    b = (b - vars505[1] + 256) & 0xFF;
    c = (c + vars505[2]) & 0xFF;
    d = (d - vars505[3] + 256) & 0xFF;

    this.var68a=a; this.var68b=b; this.var68c=c; this.var68d=d; this.bpVar=bp;
  }
};

// ─────────────────────────────────────────────────────────
// ENGINE: SS3 — LSDscreensaver3.cs port
// 320×200 direct, lookup values halved (>>1)
// c/d increment by 1 per row (softer gradient)
// ─────────────────────────────────────────────────────────
const SS3 = {
  name: 'LSD·3',
  W: 320, H: 200,
  var68a:0, var68b:0, var68c:0, var68d:0,
  vars505: new Uint8Array([2,1,3,4]),
  bpVar: 0,
  vars: new Uint8Array(320*200),

  reset() {
    this.var68a=0; this.var68b=0; this.var68c=0; this.var68d=0;
    this.vars505.set([2,1,3,4]);
    this.bpVar=0;
    this.vars.fill(0);
  },

  frame(pix32) {
    const {W, H, vars, vars505} = this;
    const LUT = LSD_LUT, LUTL = LSD_LUT_LEN;
    let {var68a:a, var68b:b, var68c:c, var68d:d, bpVar:bp} = this;
    const bpLo = bp & 0xFF;

    for (let y = 0; y < H; y++) {
      for (let x = 0; x < W; x++) {
        // halved lookup values (>>1 matches C# >> 1)
        let al = bpLo;
        al = (al + (LUT[y % LUTL] >> 1)) & 0xFF;
        al = (al + (LUT[x % LUTL] >> 1)) & 0xFF;
        al = (al + (LUT[(y+2) % LUTL] >> 1)) & 0xFF;
        al = (al + (LUT[(x+1) % LUTL] >> 1)) & 0xFF;
        al = (al | 0x80) & 0xFF;
        const ci = (al - 128) & 0x7F;
        vars[y*W+x] = ci;
        pix32[y*W+x] = LSD_PAL32[ci];
      }
      // c/d increment by 1 (not 2/1 like SS4)
      c = (c+1) & 0xFF;
      d = (d+1) & 0xFF;
    }
    // Note: a/b not used in per-pixel loop in this variant
    a = (a+1) & 0xFF;
    b = (b+2) & 0xFF;

    bp = (bp - 1) & 0xFFFF;
    let bl = bp & 0xFF;
    bl = (bl ^ ((bp >> 8) & 0xFF)) & 0xFF;
    bl = (bl ^ vars[199*W+319]) & 0xFF;
    bl = (bl ^ c) & 0xFF;
    bl = (bl ^ a) & 0xFF;
    bl = (bl + d) & 0xFF;
    bl = (bl + b) & 0xFF;
    if ((bl & 0x08) === 0) { const di=bl&3; if(vars505[di]<0xFD) vars505[di]++; }
    else                   { const di=bl&3; if(vars505[di]>0x03) vars505[di]--; }
    a = (a + vars505[0]) & 0xFF;
    b = (b - vars505[1] + 256) & 0xFF;
    c = (c + vars505[2]) & 0xFF;
    d = (d - vars505[3] + 256) & 0xFF;

    this.var68a=a; this.var68b=b; this.var68c=c; this.var68d=d; this.bpVar=bp;
  }
};

// ─────────────────────────────────────────────────────────
// ENGINE: SS4 — LSDscreensaver4.cs port
// 320×200 direct, full lookup values, original increments
// Canonical faithful port — identical to original CS logic
// ─────────────────────────────────────────────────────────
const SS4 = {
  name: 'LSD·4',
  W: 320, H: 200,
  var68a:0, var68b:0, var68c:0, var68d:0,
  vars505: new Uint8Array([2,1,3,4]),
  bpVar: 0,
  vars: new Uint8Array(320*200),

  reset() {
    this.var68a=0; this.var68b=0; this.var68c=0; this.var68d=0;
    this.vars505.set([2,1,3,4]);
    this.bpVar=0;
    this.vars.fill(0);
  },

  frame(pix32) {
    const {W, H, vars, vars505} = this;
    const LUT = LSD_LUT, LUTL = LSD_LUT_LEN;
    let {var68a:a, var68b:b, var68c:c, var68d:d, bpVar:bp} = this;
    const bpLo = bp & 0xFF;

    for (let y = 0; y < H; y++) {
      for (let x = 0; x < W; x++) {
        let al = bpLo;
        al = (al + LUT[y % LUTL]) & 0xFF;
        al = (al + LUT[x % LUTL]) & 0xFF;
        al = (al + LUT[(y+2) % LUTL]) & 0xFF;
        al = (al + LUT[(x+1) % LUTL]) & 0xFF;
        al = (al | 0x80) & 0xFF;
        const ci = (al - 128) & 0x7F;
        vars[y*W+x] = ci;
        pix32[y*W+x] = LSD_PAL32[ci];
        a = (a+1) & 0xFF;
        b = (b+3) & 0xFF;
      }
      c = (c+2) & 0xFF;
      d = (d+1) & 0xFF;
    }

    bp = (bp - 1) & 0xFFFF;
    let bl = bp & 0xFF;
    bl = (bl ^ ((bp >> 8) & 0xFF)) & 0xFF;
    bl = (bl ^ vars[199*W+319]) & 0xFF;
    bl = (bl ^ c) & 0xFF;
    bl = (bl ^ a) & 0xFF;
    bl = (bl + d) & 0xFF;
    bl = (bl + b) & 0xFF;
    if ((bl & 0x08) === 0) { const di=bl&3; if(vars505[di]<0xFD) vars505[di]++; }
    else                   { const di=bl&3; if(vars505[di]>0x03) vars505[di]--; }
    a = (a + vars505[0]) & 0xFF;
    b = (b - vars505[1] + 256) & 0xFF;
    c = (c + vars505[2]) & 0xFF;
    d = (d - vars505[3] + 256) & 0xFF;

    this.var68a=a; this.var68b=b; this.var68c=c; this.var68d=d; this.bpVar=bp;
  }
};

// ─────────────────────────────────────────────────────────
// SCREENSAVER SWITCHER
// Wraps SS engines; integrates with existing SS lifecycle.
// Patches SS.frame at runtime — SS.start/stop/idle unchanged.
// ─────────────────────────────────────────────────────────
const SS_ENGINES = [
  { id: 'fluid', label: 'Fluid',  engine: null   },  // index 0 = original SS fluid
  { id: 'lsd1',  label: 'LSD·1', engine: SS1    },  // index 1
  { id: 'lsd2',  label: 'LSD·2', engine: SS2    },  // index 2
  { id: 'lsd3',  label: 'LSD·3', engine: SS3    },  // index 3
  { id: 'lsd4',  label: 'LSD·4', engine: SS4    },  // index 4
];

let _currentEngineIdx = 0;
let _lsdCanvas  = null;  // dedicated canvas for LSD engines
let _lsdCtx     = null;
let _lsdImgData = null;
let _lsdPix32   = null;
let _lsdTimer   = null;

// Stash original SS.frame so we can restore it
const _SS_FLUID_FRAME = SS.frame.bind(SS);
const _SS_FLUID_START = SS.start.bind(SS);
const _SS_FLUID_STOP  = SS.stop.bind(SS);

function _lsdInit() {
  if (_lsdCanvas) return;
  _lsdCanvas = document.createElement('canvas');
  _lsdCanvas.width  = 320;
  _lsdCanvas.height = 200;
  Object.assign(_lsdCanvas.style, {
    position: 'fixed', inset: '0', width: '100vw', height: '100vh',
    zIndex: '200', display: 'none', imageRendering: 'pixelated',
    cursor: 'none', background: '#000'
  });
  document.body.appendChild(_lsdCanvas);
  _lsdCtx     = _lsdCanvas.getContext('2d');
  _lsdImgData = _lsdCtx.createImageData(320, 200);
  // Uint32Array view over the same buffer — direct 32-bit pixel writes
  _lsdPix32   = new Uint32Array(_lsdImgData.data.buffer);

  ['click','touchstart'].forEach(ev =>
    _lsdCanvas.addEventListener(ev, () => selectSSEngine(_currentEngineIdx), {passive:true})
  );
  document.addEventListener('keydown', () => {
    if (_lsdCanvas.style.display !== 'none') selectSSEngine(_currentEngineIdx);
  });
}

function _lsdStart(engineObj) {
  _lsdInit();
  engineObj.reset();
  _lsdCanvas.style.display = 'block';
  document.body.style.cursor = 'none';
  clearInterval(_lsdTimer);
  _lsdTimer = setInterval(() => {
    engineObj.frame(_lsdPix32);
    _lsdCtx.putImageData(_lsdImgData, 0, 0);
  }, 66);
}

function _lsdStop() {
  clearInterval(_lsdTimer);
  if (_lsdCanvas) _lsdCanvas.style.display = 'none';
  document.body.style.cursor = '';
}

// Public: switch to engine by index. Called by buttons + cycling.
function selectSSEngine(idx) {
  // If a screensaver is currently active, stop everything first
  const fluidActive = SS.active;
  const lsdActive   = _lsdCanvas && _lsdCanvas.style.display !== 'none';

  if (fluidActive) { SS.active = false; clearInterval(SS.timer); SS.canvas.classList.add('hidden'); document.body.style.cursor = ''; }
  if (lsdActive)   _lsdStop();

  _currentEngineIdx = ((idx % SS_ENGINES.length) + SS_ENGINES.length) % SS_ENGINES.length;

  // Update button highlights
  document.querySelectorAll('.ss-engine-btn').forEach((b,i) => {
    b.classList.toggle('active', i === _currentEngineIdx);
  });

  // If something was playing, restart with new engine
  if (fluidActive || lsdActive) {
    _startCurrentEngine();
  }

  SS.resetIdleTimer();
}

function _startCurrentEngine() {
  const e = SS_ENGINES[_currentEngineIdx];
  if (e.engine === null) {
    // Original fluid screensaver
    SS.active = true;
    SS.reset();
    SS.canvas.classList.remove('hidden');
    document.body.style.cursor = 'none';
    SS.timer = setInterval(() => SS.frame(), 44);
  } else {
    _lsdStart(e.engine);
  }
}

// Patch SS.start to route through current engine selection
SS.start = function() {
  if (this.active) return;
  const e = SS_ENGINES[_currentEngineIdx];
  if (e.engine === null) {
    _SS_FLUID_START();
  } else {
    this.active = true;   // mark active so stop() works correctly
    _lsdStart(e.engine);
  }
};

// Patch SS.stop to also kill LSD canvas
SS.stop = function() {
  if (!this.active) return;
  this.active = false;
  clearInterval(this.timer);
  this.canvas.classList.add('hidden');
  _lsdStop();
  document.body.style.cursor = '';
  this.resetIdleTimer();
};

// ─────────────────────────────────────────────────────────
// UI INJECTION
// Adds engine selector row inside #ss-speed-panel,
// below the existing timeout buttons.
// ─────────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
  const panel = document.getElementById('ss-speed-panel');
  if (!panel) return;

  const row = document.createElement('div');
  row.innerHTML = `
    <p class="text-xs text-gray-500 mt-3 mb-2 font-semibold tracking-wide uppercase">
      <i class="fa-solid fa-film text-blue-400 mr-1"></i>
      Screensaver style
    </p>
    <div class="flex gap-1 flex-wrap" id="ss-engine-row"></div>
  `;
  panel.appendChild(row);

  const container = document.getElementById('ss-engine-row');
  SS_ENGINES.forEach((e, i) => {
    const btn = document.createElement('button');
    btn.className = 'speed-btn ss-engine-btn' + (i === 0 ? ' active' : '');
    btn.textContent = e.label;
    btn.dataset.idx = i;
    btn.onclick = () => selectSSEngine(i);
    container.appendChild(btn);
  });

  _lsdInit();
});
</script>
<!-- ═══════════════════════════════════════════════════════
     FLUID·2 ENGINE — LsdFluid.cs exact port
     Drop this immediately after the last LSD </script> tag.
     Uses LSD_PAL32 / LSD_PAL already defined above.
     ═══════════════════════════════════════════════════════ -->
<script>
const SS_FLUID2 = {
  name: 'Fluid·2',

  SW: 160, SH: 100,
  BW: 320, BH: 200,

  density: null, velX: null, velY: null,
  wave: null, wavePrev: null, waveNext: null,
  orbX: 0, orbY: 0, orbVX: 0, orbVY: 0.55,
  gravCX: 0, gravCY: 0,
  flamePhase: 0,
  frameTime: 0,

  // colorBytes equivalent — [r, g, b] per palette index
  // uses LSD_PAL already in scope (no extra data)

  reset() {
    const {SW, SH, BW, BH} = this;
    this.density  = new Float32Array(SW * SH);
    this.velX     = new Float32Array(SW * SH);
    this.velY     = new Float32Array(SW * SH);
    this.wave     = new Float32Array(BW * BH);
    this.wavePrev = new Float32Array(BW * BH);
    this.waveNext = new Float32Array(BW * BH);

    for (let y = 0; y < SH; y++) {
      for (let x = 0; x < SW; x++) {
        const fx = x / SW, fy = y / SH;
        const i  = y * SW + x;
        this.density[i] = Math.sin(fx * 10 + fy * 10) * 1.5 + 0.5;
        this.velX[i]    = Math.sin(fx * 15) * 0.1;
        this.velY[i]    = Math.cos(fy * 15) * 0.1;
      }
    }

    this.gravCX = SW * 0.5;
    this.gravCY = SH * 0.5;
    this.orbX   = this.gravCX + SW * 0.28;
    this.orbY   = this.gravCY - SH * 0.08;
    this.orbVX  = 0;
    this.orbVY  = 0.55;
    this.flamePhase = 0;
    this.frameTime  = 0;
  },

  // ── SpawnRainImpact — Gaussian splat + inverse crown (LsdFluid.cs exact) ──
  spawnRainImpact() {
    const {BW, BH} = this;
    const x      = (Math.random() * (BW - 2) + 1) | 0;
    const y      = (Math.random() * (BH - 2) + 1) | 0;
    const energy = 8 + Math.random() * 12;

    // Gaussian splat r=6
    for (let dy = -6; dy <= 6; dy++) {
      for (let dx = -6; dx <= 6; dx++) {
        const nx = x + dx, ny = y + dy;
        if (nx < 1 || ny < 1 || nx >= BW - 1 || ny >= BH - 1) continue;
        const r2 = dx*dx + dy*dy;
        this.wave[ny * BW + nx] += energy * Math.exp(-r2 * 0.12);
      }
    }

    // Inverse crown ring at r≈4 (matches C# angle step 0.3)
    for (let a = 0; a < Math.PI * 2; a += 0.3) {
      const rx = (x + Math.cos(a) * 4 + 0.5) | 0;
      const ry = (y + Math.sin(a) * 4 + 0.5) | 0;
      if (rx < 1 || ry < 1 || rx >= BW - 1 || ry >= BH - 1) continue;
      this.wave[ry * BW + rx] -= energy * 0.3;
    }
  },

  // ── UpdateWaves — 4-neighbor *0.5 + Laplacian *0.003 (LsdFluid.cs exact) ──
  updateWaves() {
    const {BW, BH} = this;
    const damp = 0.985, clamp = 24;

    for (let y = 1; y < BH - 1; y++) {
      for (let x = 1; x < BW - 1; x++) {
        const i  = y * BW + x;
        const N  = this.wave[(y-1)*BW + x];
        const S  = this.wave[(y+1)*BW + x];
        const E  = this.wave[y*BW + (x+1)];
        const W  = this.wave[y*BW + (x-1)];
        const nb = N + S + E + W;

        // 4-neighbor stencil * 0.5 (C# uses * 0.5, not 0.30/0.175)
        let v = nb * 0.5 - this.wavePrev[i];
        v *= damp;
        // additive Laplacian surface tension (* 0.003, not subtracted)
        v += nb * 0.003;
        if (v >  clamp) v =  clamp;
        if (v < -clamp) v = -clamp;
        this.waveNext[i] = v;
      }
    }

    const tmp     = this.wavePrev;
    this.wavePrev = this.wave;
    this.wave     = this.waveNext;
    this.waveNext = tmp;
  },

  // ── CreateGradientBackground — radial lerp center/outer (LsdFluid.cs exact) ──
  _buildBackground(bgBuf) {
    const {BW, BH} = this;
    const tt   = this.frameTime * 0.003;   // matches SS fluid time scale
    const cx2  = BW / 2, cy2 = BH / 2;
    const maxR = Math.sqrt(cx2*cx2 + cy2*cy2);

    // outer (sin) and inner (cos) — same formula as C# CreateGradientBackground
    const outerR = (Math.sin(tt)     * 127 + 128) | 0;
    const outerG = (Math.sin(tt + 2) * 127 + 128) | 0;
    const outerB = (Math.sin(tt + 4) * 127 + 128) | 0;
    const innerR = (Math.cos(tt)     * 127 + 128) | 0;
    const innerG = (Math.cos(tt + 2) * 127 + 128) | 0;
    const innerB = (Math.cos(tt + 4) * 127 + 128) | 0;

    for (let y = 0; y < BH; y++) {
      for (let x = 0; x < BW; x++) {
        const dx = x - cx2, dy = y - cy2;
        const tv = Math.min(Math.sqrt(dx*dx + dy*dy) / maxR, 1);
        const r  = (outerR + (innerR - outerR) * tv) | 0;
        const g  = (outerG + (innerG - outerG) * tv) | 0;
        const b  = (outerB + (innerB - outerB) * tv) | 0;
        // pack as ABGR Uint32 (LE canvas)
        bgBuf[y * BW + x] = (0xFF000000) | (b << 16) | (g << 8) | r;
      }
    }
  },

  frame(pix32) {
    const {SW, SH, BW, BH} = this;
    const t  = this.frameTime * 0.35;

    // ── Orbital gravity (identical to SS fluid) ──
    const GM = 18, soft = 4, drag = 0.9995;
    const dxG = this.gravCX - this.orbX;
    const dyG = this.gravCY - this.orbY;
    const distSq  = dxG*dxG + dyG*dyG + soft*soft;
    const distInv = 1 / Math.sqrt(distSq);
    const force   = GM * distInv * distInv;
    this.orbVX += dxG * distInv * force;
    this.orbVY += dyG * distInv * force;
    this.orbVX *= drag; this.orbVY *= drag;
    this.orbX  += this.orbVX;
    this.orbY  += this.orbVY;

    const margin = 6;
    if (this.orbX < margin)    { this.orbX=margin;    this.orbVX= Math.abs(this.orbVX)*0.6; }
    if (this.orbX > SW-margin) { this.orbX=SW-margin; this.orbVX=-Math.abs(this.orbVX)*0.6; }
    if (this.orbY < margin)    { this.orbY=margin;    this.orbVY= Math.abs(this.orbVY)*0.6; }
    if (this.orbY > SH-margin) { this.orbY=SH-margin; this.orbVY=-Math.abs(this.orbVY)*0.6; }

    const cx = this.orbX | 0;
    const cy = this.orbY | 0;

    // ── Flame injection (identical to SS fluid) ──
    const radius = 8;
    for (let dy = -radius; dy <= radius; dy++) {
      for (let dx = -radius; dx <= radius; dx++) {
        const nx = cx+dx, ny = cy+dy;
        if (nx<0||ny<0||nx>=SW||ny>=SH) continue;
        const dist  = Math.sqrt(dx*dx+dy*dy);
        const angle = Math.atan2(dy, dx);
        const edge  = radius
          + Math.sin(angle*5  + this.flamePhase)     * 2.5
          + Math.sin(angle*9  - this.flamePhase*1.7) * 1.5
          + Math.sin(angle*13 + this.flamePhase*0.7) * 0.8;
        if (dist > edge) continue;
        const falloff = (1 - dist/edge) ** 2;
        const i = ny*SW+nx;
        this.density[i] += 0.8 * falloff;
        const swirl = Math.sin(angle*7 + this.flamePhase*3);
        this.velX[i] += -dy * 0.03 * swirl * falloff;
        this.velY[i] +=  dx * 0.03 * swirl * falloff;
      }
    }

    // ── Vortex core (identical to SS fluid) ──
    for (let dy = -10; dy <= 10; dy++) {
      for (let dx = -10; dx <= 10; dx++) {
        const nx=cx+dx, ny=cy+dy;
        if (nx<1||ny<1||nx>=SW-1||ny>=SH-1) continue;
        const r2 = dx*dx+dy*dy;
        if (r2 > 100) continue;
        const inv = 1/(1+r2*0.08);
        this.velX[ny*SW+nx] += -dy * inv * 0.05;
        this.velY[ny*SW+nx] +=  dx * inv * 0.05;
      }
    }
    this.flamePhase += 0.18;

    // ── Advection (identical to SS fluid, with clamp — C# has clamp here) ──
    const newDensity = new Float32Array(SW * SH);
    for (let y = 1; y < SH-1; y++) {
      for (let x = 1; x < SW-1; x++) {
        const i  = y*SW+x;
        const px = x - this.velX[i];
        const py = y - this.velY[i];
        const ix = Math.max(1, Math.min(SW-2, px|0));
        const iy = Math.max(1, Math.min(SH-2, py|0));
        const fx = px-ix, fy = py-iy;
        const d =
          (1-fx)*(1-fy)*this.density[iy*SW+ix]     +
          fx    *(1-fy)*this.density[iy*SW+ix+1]   +
          (1-fx)*fy    *this.density[(iy+1)*SW+ix] +
          fx    *fy    *this.density[(iy+1)*SW+ix+1];
        newDensity[i] = Math.max(0, Math.min(1, d));  // C# clamps here
      }
    }
    this.density = newDensity;

    // ── Global diffusion pass ──
    for (let y = 1; y < SH-1; y++) {
      for (let x = 1; x < SW-1; x++) {
        const i = y*SW+x;
        this.density[i] = (this.density[i] +
          (this.density[i-1] + this.density[i+1] +
           this.density[i-SW] + this.density[i+SW]) * 0.2) * 0.5;
      }
    }

    // ── Localised 3-pass r=14 smoothing around orb — NEW vs SS fluid ──
    for (let pass = 0; pass < 3; pass++) {
      const yMin = Math.max(1,    cy - 14);
      const yMax = Math.min(SH-2, cy + 14);
      const xMin = Math.max(1,    cx - 14);
      const xMax = Math.min(SW-2, cx + 14);
      for (let y = yMin; y <= yMax; y++) {
        for (let x = xMin; x <= xMax; x++) {
          const ddx = x-cx, ddy = y-cy;
          if (ddx*ddx + ddy*ddy > 196) continue;   // r=14, 14²=196
          const i = y*SW+x;
          this.density[i] =
            (this.density[i-1] + this.density[i+1] +
             this.density[i-SW] + this.density[i+SW]) * 0.25;
        }
      }
    }

    // ── Background: radial gradient (LsdFluid.cs CreateGradientBackground) ──
    // Reuse pix32 as bg scratch then composite over it in place
    this._buildBackground(pix32);

    // ── Composite: wave refraction + fluid blend ──
    // We need the background values before we overwrite them with fluid pixels.
    // Copy bg to a temp buffer first (same size, cheap typed array copy).
    if (!this._bgBuf) this._bgBuf = new Uint32Array(BW * BH);
    this._bgBuf.set(pix32);
    const bg = this._bgBuf;

    for (let y = 0; y < BH; y++) {
      for (let x = 0; x < BW; x++) {
        const wIdx = y*BW+x;
        const wnx  = (x < BW-1 ? this.wave[wIdx+1]     : 0) - (x > 0 ? this.wave[wIdx-1]     : 0);
        const wny  = (y < BH-1 ? this.wave[(y+1)*BW+x] : 0) - (y > 0 ? this.wave[(y-1)*BW+x] : 0);
        const rx   = Math.max(0, Math.min(BW-1, (x + wnx * 1.8) | 0));
        const ry   = Math.max(0, Math.min(BH-1, (y + wny * 1.8) | 0));

        // sample bg at refracted coords
        const bgPx = bg[ry*BW+rx];
        const bgR  = bgPx        & 0xFF;
        const bgG  = (bgPx >> 8) & 0xFF;
        const bgB  = (bgPx >>16) & 0xFF;

        // bilinear density sample
        const sxf = x * (SW-1) / (BW-1);
        const syf = y * (SH-1) / (BH-1);
        const sx0 = sxf | 0, sy0 = syf | 0;
        const sx1 = Math.min(sx0+1, SW-1), sy1 = Math.min(sy0+1, SH-1);
        const sfx = sxf-sx0, sfy = syf-sy0;
        const d =
          this.density[sy0*SW+sx0]*(1-sfx)*(1-sfy) +
          this.density[sy0*SW+sx1]*sfx    *(1-sfy) +
          this.density[sy1*SW+sx0]*(1-sfx)*sfy     +
          this.density[sy1*SW+sx1]*sfx    *sfy;

        // palette lookup — LSD palette, same shimmer term as C# (sin(t*0.05)*0.2)
        const ci  = ((d + 0.2 * Math.sin(t * 0.05)) * 127) & 0x7F;
        const pal = LSD_PAL[ci];   // [r,g,b] — no extra data needed
        const alpha = Math.max(0, Math.min(1, d));
        const ia    = 1 - alpha;

        const r = (pal[0] * alpha + bgR * ia) | 0;
        const g = (pal[1] * alpha + bgG * ia) | 0;
        const b = (pal[2] * alpha + bgB * ia) | 0;
        pix32[y*BW+x] = (0xFF000000) | (b<<16) | (g<<8) | r;
      }
    }

    this.frameTime++;

    // ── Rain + wave propagation ──
    if (Math.random() < 0.4) this.spawnRainImpact();
    this.updateWaves();
  }
};

// ── Register Fluid·2 in SS_ENGINES ──────────────────────
// SS_ENGINES was defined in the previous script block.
// Push after the existing 5 entries.
SS_ENGINES.push({ id: 'fluid2', label: 'Fluid·2', engine: SS_FLUID2 });

// Re-render the engine button row to include the new entry.
// DOMContentLoaded already fired, so patch the row directly.
(function _addFluid2Btn() {
  function inject() {
    const row = document.getElementById('ss-engine-row');
    if (!row) { setTimeout(inject, 100); return; }
    const idx = SS_ENGINES.length - 1;   // 5
    const btn = document.createElement('button');
    btn.className    = 'speed-btn ss-engine-btn';
    btn.textContent  = 'Fluid·2';
    btn.dataset.idx  = idx;
    btn.onclick      = () => selectSSEngine(idx);
    row.appendChild(btn);
  }
  if (document.readyState === 'loading')
    document.addEventListener('DOMContentLoaded', inject);
  else
    inject();
})();
</script>
</body>
</html>
