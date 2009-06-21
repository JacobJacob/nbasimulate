#!/usr/bin/perl
use RRDs;
use LWP::UserAgent;

# define location of rrdtool databases
my $rrd = '/data/webauth_working/www/rrd';
# define location of images
my $img = '/data/webauth_working/www/rrd';
# define your nginx stats URL
my $URL = "http://127.0.0.1/nginx_status";

my $ua = LWP::UserAgent->new(timeout => 30);
my $response = $ua->request(HTTP::Request->new('GET', $URL));

my $requests = 0;
my $total =  0;
my $reading = 0;
my $writing = 0;
my $waiting = 0;

#print $response->content;
foreach (split(/\n/, $response->content)) {
  $total = $1 if (/^Active connections:\s+(\d+)/);
  if (/^Reading:\s+(\d+).*Writing:\s+(\d+).*Waiting:\s+(\d+)/) {
    $reading = $1;
    $writing = $2;
    $waiting = $3;
  }
  $requests = $3 if (/^\s+(\d+)\s+(\d+)\s+(\d+)/);
}

#print "RQ:$requests; TT:$total; RD:$reading; WR:$writing; WA:$waiting\n";

# if rrdtool database doesn't exist, create it
if (! -e "$rrd/nginx.rrd") {
  RRDs::create "$rrd/nginx.rrd",
        "-s 60",
    "DS:requests:COUNTER:120:0:100000000",
    "DS:total:GAUGE:120:0:60000",
    "DS:reading:GAUGE:120:0:60000",
    "DS:writing:GAUGE:120:0:60000",
    "DS:waiting:GAUGE:120:0:60000",
    "RRA:AVERAGE:0.5:1:2880",
    "RRA:AVERAGE:0.5:30:672",
    "RRA:AVERAGE:0.5:120:732",
    "RRA:AVERAGE:0.5:720:1460";
}

# insert values into rrd database
RRDs::update "$rrd/nginx.rrd",
  "-t", "requests:total:reading:writing:waiting",
  "N:$requests:$total:$reading:$writing:$waiting";

# Generate graphs
CreateGraphs("10minute");
CreateGraphs("1hour");
CreateGraphs("1day");
CreateGraphs("1week");
CreateGraphs("1month");
CreateGraphs("1year");

#------------------------------------------------------------------------------
sub CreateGraphs($){
  my $period = shift;
  my ($sec,$min,$hour,$mday,$mon,$year,$wday,$yday,$isdst)=localtime(time); 
  my $time_stamp = sprintf("%4d-%02d-%02d %02d:%02d:%02d",$year+1900,$mon+1,$mday,$hour,$min,$sec);
  
  RRDs::graph "$img/requests-$period.png",
        "-s -$period",
        "-t [$time_stamp] Requests in $period",
        "--lazy",
        "-h", "150", "-w", "700",
        "-l 0",
        "-a", "PNG",
        "-v requests/sec",
        "DEF:requests=$rrd/nginx.rrd:requests:AVERAGE",
        "LINE2:requests#336600:Requests",
        "GPRINT:requests:LAST: Current\\: %5.1lf %S",
        "GPRINT:requests:MIN:  Min\\: %5.1lf %S",
        "GPRINT:requests:AVERAGE: Avg\\: %5.1lf %S",
        "GPRINT:requests:MAX:  Max\\: %5.1lf %S req/sec",
        "HRULE:0#000000";
  if ($ERROR = RRDs::error) { 
    print "$0: unable to generate $period graph: $ERROR\n"; 
  }

  RRDs::graph "$img/connections-$period.png",
        "-s -$period",
        "-t [$time_stamp] Connections in $period",
        "--lazy",
        "-h", "150", "-w", "700",
        "-l 0",
        "-a", "PNG",
        "-v connections/sec",
        "DEF:total=$rrd/nginx.rrd:total:AVERAGE",
        "DEF:reading=$rrd/nginx.rrd:reading:AVERAGE",
        "DEF:writing=$rrd/nginx.rrd:writing:AVERAGE",
        "DEF:waiting=$rrd/nginx.rrd:waiting:AVERAGE",

        "LINE2:total#22FF22:Total",
        "GPRINT:total:LAST:   Current\\: %5.1lf %S",
        "GPRINT:total:MIN:  Min\\: %5.1lf %S",
        "GPRINT:total:AVERAGE: Avg\\: %5.1lf %S",
        "GPRINT:total:MAX:  Max\\: %5.1lf %S conn/sec\\n",
        
        "LINE2:reading#0022FF:Reading",
        "GPRINT:reading:LAST: Current\\: %5.1lf %S",
        "GPRINT:reading:MIN:  Min\\: %5.1lf %S",
        "GPRINT:reading:AVERAGE: Avg\\: %5.1lf %S",
        "GPRINT:reading:MAX:  Max\\: %5.1lf %S conn/sec\\n",
        
        "LINE2:writing#FF0000:Writing",
        "GPRINT:writing:LAST: Current\\: %5.1lf %S",
        "GPRINT:writing:MIN:  Min\\: %5.1lf %S",
        "GPRINT:writing:AVERAGE: Avg\\: %5.1lf %S",
        "GPRINT:writing:MAX:  Max\\: %5.1lf %S conn/sec\\n",
        
        "LINE2:waiting#00AAAA:Waiting",
        "GPRINT:waiting:LAST: Current\\: %5.1lf %S",
        "GPRINT:waiting:MIN:  Min\\: %5.1lf %S",
        "GPRINT:waiting:AVERAGE: Avg\\: %5.1lf %S",
        "GPRINT:waiting:MAX:  Max\\: %5.1lf %S conn/sec\\n",

        "HRULE:0#000000";
  if ($ERROR = RRDs::error) { 
    print "$0: unable to generate $period graph: $ERROR\n"; 
  }
}
