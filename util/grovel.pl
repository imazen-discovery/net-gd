#!/usr/bin/perl

# quick-and-dirty script to grovel through static image functions and
# produce wrappers in class Image.

#grep  '^ *public static' LibGD.cs | grep '{' | grep '(SWIGTYPE_p_gdImageStruct' | perl -ne 's/\) \{$//'; m/(\w+) \( SWIGTYPE_p_gdImageStruct\s+\w+,\s*(.*)/ or die "xxx"; 

# 219

use strict;
use warnings;

while(<>) {

  next unless s{\) \s* \{ \s* \z}{}x;
  next unless m{\w+ \( SWIGTYPE_p_gdImageStruct \s}x;
$DB::single = 1;
  next unless m{^ \s* public \s+ static \s+ (\w+) \s+ (\w+)}x;
  my ($type, $name) = ($1, $2);

  m{\( SWIGTYPE_p_gdImageStruct \s+ \w+ (.*) \z}x
    or die "can't parse '$_'.";
  my ($args) = $1;
  $args =~ s/^\s* \, \s*//x;

  my @passedargs = split(/,\s*/, $args);
  @passedargs = map {s/^\w+\s*//; $_} @passedargs;
  my $newargs = join(", ", @passedargs);
  my $return = $type eq 'void' ? "" : "return ";
  my $cma = $args ? "," : "";

  print <<"EOF";
    private $type $name($args) {
      ${return}LibGD.$name(img$cma $newargs);
    }

EOF
  ;
}
